using Core.Components;
using Core.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ContractManagement
{
    public class ContractDefinitionProvider : IContractDefinitionProvider
    {
        public List<ContractInfo> ContractDefinitions { get; }

        public ContractDefinitionProvider()
        {
            ContractDefinitions = new List<ContractInfo>();
        }

        public void ReadAllContracts(string contractsDir)
        {
            var fileNames = Directory.EnumerateFiles(contractsDir);
            foreach (var fileName in fileNames.Where(f => f.EndsWith(".json")))
            {
                ReadContractFromFile(fileName);
            }
        }

        private void ReadContractFromFile(string filePath)
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
