using Core.Models;
using System.Collections.Generic;

namespace Core.Components
{
    public interface IContractDefinitionProvider
    {
        List<ContractInfo> ContractDefinitions { get; }
        void ReadAllContracts(string filePath);
    }
}