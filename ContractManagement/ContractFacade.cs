using Core.Components;
using Core.Exceptions;
using Core.Models;
using Core.Options;
using Microsoft.Extensions.Options;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractManagement
{
    public class ContractFacade : IContractFacade
    {
        private readonly HexBigInteger _gas = new HexBigInteger(6721975);

        private readonly Web3 _web3;
        private readonly AccountOptions _account;
        private readonly IContractStore _contractStore;
        private readonly IContractDefinitionProvider _contractProvider;
        private readonly IGetterStore _getterStore;

        public ContractFacade(IOptionsMonitor<NetworkOptions> optionsAccessor,
            IAccountProvider accountProvider, 
            IContractStore contractStore,
            IContractDefinitionProvider contractProvider,
            IGetterStore getterStore)
        {
            _web3 = new Web3(optionsAccessor.CurrentValue.Address);
            _account = accountProvider.GetAccount();
            _contractStore = contractStore;
            _contractProvider = contractProvider;
            _getterStore = getterStore;
        }

        public async Task ReleaseAllContracts()
        {
            foreach (var contractDefinition in _contractProvider.ContractDefinitions)
            {
                await ReleaseContract(contractDefinition.Name, contractDefinition.Abi, contractDefinition.Bytecode, (int)_gas.Value);
            }
        }

        public async Task<decimal> GetBalance(string address)
        {
            var balance = await _web3.Eth.GetBalance.SendRequestAsync(address);
            return Web3.Convert.FromWei(balance.Value, 18);
        }

        public async Task<string[]> GetAllContractsNames()
        {
            return (await _contractStore.GetAll()).Select(c => c.Name).ToArray();
        }

        public async Task<string> ExecuteMethod(string name, string contractMethod, Dictionary<string, string> parameters) //TODO: add parameters validation
        {
            Function method = await GetFunction(name, contractMethod);
            try
            {
                object[] parametersObjects = parameters.Values.Select(p =>
                {
                    if (int.TryParse(p, out int parsed))
                    {
                        return (object) parsed;
                    }
                    else
                    {
                        return (object) p;
                    }
                }).ToArray<object>();
                //var callResult = await method.CallAsync<object>(parametersObjects);
                var result = await method.SendTransactionAndWaitForReceiptAsync(_account.Address, _gas, null, null, parametersObjects);
                return result.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<object> InvokeGetComplex(string name, string contractMethod, Dictionary<string, object> parameters)
        {
            Function method = await GetFunction(name, contractMethod);
            try
            {
                object result = _getterStore.InvokeGetComplex(name, contractMethod, method, parameters);
                return result;
            }
            catch (Exception ex)
            {
                throw new ContractException("Error occured during method executing", ex);
            }
        }

        public async Task<TReturn> InvokeGetSimpleType<TReturn>(string name, string contractMethod, Dictionary<string, object> parameters)
        {
            Function method = await GetFunction(name, contractMethod);
            try
            {
                return await method.CallAsync<TReturn>(parameters?.Values.ToArray());
            }
            catch (Exception ex)
            {
                throw new ContractException("Error occured during method executing", ex);
            }
        }

        private async Task<Function> GetFunction(string name, string contractMethod)
        {
            var contract = await GetContract(name);

            Function method = contract.GetFunction(contractMethod);
            return method;
        }

        public async Task<string> TryGetContractAddress(string name)
        {
            var existing = await GetContractFromStorage(name);
            if (existing == null)
            {
                throw new ApplicationException($"Contract {name} does not exist in storage");
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

        private async Task SaveContract(ContractInfo contract) => await _contractStore.Save(contract);

        private async Task<ContractInfo> GetContractFromStorage(string name) => await _contractStore.Get(name);

        private async Task<Contract> GetContract(string name)
        {
            var existing = await GetContractFromStorage(name);
            if (existing == null)
            {
                throw new ApplicationException($"Contract {name} does not exist in storage");
            }

            if (existing.ContractAddress == null)
            {
                throw new ApplicationException($"Contract address for {name} is empty.");
            }

            if (await UnlockAccount())
            {
                return _web3.Eth.GetContract(existing.Abi, existing.ContractAddress);
            }
            else
            {
                throw new ApplicationException($"Could not unlock given account: {_account.Address}");
            }
        }

        private async Task<bool> UnlockAccount()
        {
            try
            {
                return await _web3.Personal.UnlockAccount.SendRequestAsync(_account.Address, _account.Password, 60);
            }
            catch (Exception e)
            {
                throw new AccountException($"Unknown error occured while trying to unlock account with address: {_account.Address}.", e);
            }
        }

        private async Task ReleaseContract(string name, string abi, string byteCode, int gas)
        {
            var existing = await GetContractFromStorage(name);
            if (existing != null)
            {
                throw new ApplicationException($"Contract {name} is already present in storage");
            }

            if (await UnlockAccount())
            {
                try
                {
                    var receipt = await _web3.Eth.DeployContract.SendRequestAndWaitForReceiptAsync(abi, byteCode, _account.Address, new HexBigInteger(gas));

                    //var contractAddress = await GetContractAddressFromNetwork(transactionHash);

                    if (string.IsNullOrWhiteSpace(receipt?.ContractAddress))
                    {
                        throw new ContractException("Tried to read contract address after release, but got no contract address");
                    }

                    await SaveContract(new ContractInfo(name, abi, byteCode, receipt.TransactionHash, receipt.ContractAddress));
                }
                catch (Exception e)
                {
                    throw new ContractException("Failed while releasing contract", e);
                }
            }
        }

        private async Task<string> GetContractAddressFromNetwork(string transactionHash)
        {
            await UnlockAccount();
            TransactionReceipt receipt;
            try
            {
                receipt = await _web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);
            }
            catch (Exception e)
            {
                throw new ContractException($"Could not get transaction receipt for transaction hash {transactionHash} while trying to get contract address", e);
            }
           
            if (receipt != null)
            {
                return receipt.ContractAddress;
            }
            else
            {
                throw new ApplicationException($"Received null receipt from transaction: {transactionHash}");
            }
        }
    }
}
