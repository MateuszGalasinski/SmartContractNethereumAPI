using ContractManagement.Models;
using System.Threading.Tasks;

namespace ContractManagement.Components
{
    public interface IContractStore
    {
        Task<ContractInfo> Get(string name);
        Task Save(ContractInfo contract);
    }
}
