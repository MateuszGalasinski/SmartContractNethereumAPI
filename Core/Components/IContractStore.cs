using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Components
{
    public interface IContractStore
    {
        Task<IEnumerable<ContractInfo>> GetAll();
        Task<ContractInfo> Get(string name);
        Task Save(ContractInfo contract);
    }
}
