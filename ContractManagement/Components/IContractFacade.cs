using ContractManagement.Models;
using Nethereum.Contracts;
using System.Threading.Tasks;

namespace ContractManagement.Components
{
    public interface IContractFacade
    {
        Task<string> ExecuteMethod(string name, string contractMethod, int value);
        Task SaveContract(ContractInfo contract);
        Task<ContractInfo> GetContractFromStorage(string name);
        Task<decimal> GetBalance(string address);
        Task ReleaseAllContracts();
        Task<string> TryGetContractAddress(string name);
        Task<Contract> GetContract(string name);
    }
}
