using ContractManagement.Components;
using ContractManagement.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace ContractManagement
{
    public class ContractDefinitionProvider : IContractDefinitionProvider
    {
        public List<ContractInfo> ContractDefinitions { get; }

        public ContractDefinitionProvider()
        {
            ContractDefinitions = new List<ContractInfo>();
        }

        public void ReadContractFromFile(string filePath)
        {
            var contentJson = JObject.Parse(File.ReadAllText(filePath));
            ContractDefinitions.Add(new ContractInfo()
            {
                Name = contentJson["contractName"].ToString(),
                Abi = contentJson["abi"].ToString(),
                Bytecode = contentJson["bytecode"].ToString()
            });
        }
    }
}
