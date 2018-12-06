using ContractManagement.Components;
using ContractManagement.Models;
using ContractManagement.Models.Options;
using Microsoft.Extensions.Options;
using Nethereum.Contracts;
using Nethereum.Web3;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ContractManagement
{
    public class ContractFacade : IContractFacade
    {
        private const int Gas = 470000;

        private readonly Web3 _web3;
        private readonly AccountOptions _account;
        private readonly IContractStore _contractStore;
        private readonly IContractDefinitionProvider _contractProvider;

        public ContractFacade(IOptionsMonitor<NetworkOptions> optionsAccessor, IAccountProvider accountProvider, IContractStore contractStore, IContractDefinitionProvider contractProvider)
        {
            _web3 = new Web3($"{optionsAccessor.CurrentValue.Url}:{optionsAccessor.CurrentValue.Port}");
            _account = accountProvider.GetAccount();
            _contractStore = contractStore;
            _contractProvider = contractProvider;
        }

        public async Task ReleaseAllContracts()
        {
            foreach (var contractDefinition in _contractProvider.ContractDefinitions)
            {
                await ReleaseContract(contractDefinition.Name, contractDefinition.Abi, contractDefinition.Bytecode, Gas);
            }
        }

        public async Task<string[]> GetAllContractsNames()
        {
            return (await _contractStore.GetAll()).Select(c => c.Name).ToArray();
        }

        public async Task<string> ExecuteMethod(string name, string contractMethod, int value)
        {
            var contract = await GetContract(name);

            var method = contract.GetFunction(contractMethod);
            try
            {
                var result = await method.CallAsync<int>(value);
                //var result = await method.SendTransactionAsync(service.AccountAddress, value);
                return result.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task SaveContract(ContractInfo contract) => await _contractStore.Save(contract);

        public async Task<ContractInfo> GetContractFromStorage(string name) => await _contractStore.Get(name);

        public async Task<decimal> GetBalance(string address)
        {
            var balance = await _web3.Eth.GetBalance.SendRequestAsync(address);
            return Web3.Convert.FromWei(balance.Value, 18);
        }

        private async Task ReleaseContract(string name, string abi, string byteCode, int gas)
        {
            var existing = await GetContractFromStorage(name);
            if (existing != null)
            {
                throw new Exception($"Contract {name} is already present in storage");
            }

            if (await _web3.Personal.UnlockAccount.SendRequestAsync(_account.Address, _account.Password, 60))
            {
                try
                {
                    var transactionHash = await _web3.Eth.DeployContract.SendRequestAsync(abi, byteCode, _account.Address, new Nethereum.Hex.HexTypes.HexBigInteger(gas), 2);

                    var contractAddress = await GetContractAddressFromNetwork(transactionHash);

                    if (string.IsNullOrWhiteSpace(contractAddress))
                    {
                        throw new ApplicationException("Tried to release contract, but got no contract address");
                    }

                    await SaveContract(new ContractInfo(name, abi, byteCode, transactionHash, contractAddress));
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Could not release contract due to unexpected problem.", ex);
                }
            }
        }

        private async Task<string> GetContractAddressFromNetwork(string transactionHash)
        {
            var resultUnlocking = await _web3.Personal.UnlockAccount.SendRequestAsync(_account.Address, _account.Password, 60);
            if (resultUnlocking)
            {
                var receipt = await _web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);
                if (receipt != null)
                {
                    return receipt.ContractAddress;
                }
                else
                {
                    throw new ApplicationException($"Could not get contract address from transaction: {transactionHash}");
                }
            }
            else
            {
                throw new ApplicationException($"Could not unlock given account: {_account.Address}");
            }
        }

        public async Task<string> TryGetContractAddress(string name)
        {
            var existing = await GetContractFromStorage(name);
            if (existing == null)
            {
                throw new Exception($"Contract {name} does not exist in storage");
            }

            if (!string.IsNullOrEmpty(existing.ContractAddress))
            {
                return existing.ContractAddress;
            }
            else
            {
                return await GetContractAddressFromNetwork(existing.TransactionHash);
            }
        }

        public async Task<Contract> GetContract(string name)
        {
            var existing = await GetContractFromStorage(name);
            if (existing == null)
            {
                throw new Exception($"Contract {name} does not exist in storage");
            }

            if (existing.ContractAddress == null)
            {
                throw new Exception($"Contract address for {name} is empty.");
            }

            var resultUnlocking = await _web3.Personal.UnlockAccount.SendRequestAsync(_account.Address, _account.Password, 60);
            if (resultUnlocking)
            {
                return _web3.Eth.GetContract(existing.Abi, existing.ContractAddress);
            }
            else
            {
                throw new ApplicationException($"Could not unlock given account: {_account.Address}");
            }
        }
    }
}
