using System;

namespace Core.Models
{
    public class ContractInfo
    {
        public string Name { get; set; }
        public string Abi { get; set; }
        public string Bytecode { get; set; }
        public string TransactionHash { get; set; }
        public string ContractAddress { get; set; }

        public ContractInfo(string name, string abi, string bytecode, string transactionHash)
        {
            Name = name;
            Abi = abi;
            Bytecode = bytecode;
            TransactionHash = transactionHash;
        }

        public ContractInfo(string name, string abi, string bytecode, string transactionHash, string contractAddress) : this(name, abi, bytecode, transactionHash)
        {
            ContractAddress = contractAddress ?? throw new ArgumentNullException(nameof(contractAddress));
        }

        public ContractInfo()
        {
        }
    }
}
