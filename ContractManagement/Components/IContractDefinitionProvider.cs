using ContractManagement.Models;
using System.Collections.Generic;

namespace ContractManagement.Components
{
    public interface IContractDefinitionProvider
    {
        List<ContractInfo> ContractDefinitions { get; }
        void ReadContractFromFile(string filePath);
    }
}