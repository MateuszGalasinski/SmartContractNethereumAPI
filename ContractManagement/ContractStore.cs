using ContractManagement.Components;
using ContractManagement.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractManagement
{
    public class ContractStore : IContractStore
    {
        private readonly List<ContractInfo> _contracts = new List<ContractInfo>();

        public async Task<ContractInfo> Get(string name)
        {
            return await Task.Run(() => _contracts.FirstOrDefault(c => c.Name == name));
        }

        public async Task Save(ContractInfo contract)
        {
            await Task.Run(() =>
            {
                if (!_contracts.Exists(c => c.Equals(contract)))
                {
                    _contracts.Add(contract);
                }
            });
        }
    }
}
