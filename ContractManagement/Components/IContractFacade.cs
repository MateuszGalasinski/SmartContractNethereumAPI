using ContractManagement.Models;
using Nethereum.Contracts;
using System.Threading.Tasks;

namespace ContractManagement.Components
{
    public interface IContractFacade
    {
        Task ReleaseAllContracts();
        Task<string[]> GetAllContractsNames();
        Task<string> TryGetContractAddress(string name);
        Task<string> ExecuteMethod(string name, string contractMethod, int value);
        Task<decimal> GetBalance(string address);
        Task SaveContract(ContractInfo contract);
        Task<ContractInfo> GetContractFromStorage(string name);
        Task<Contract> GetContract(string name);
    }
}
