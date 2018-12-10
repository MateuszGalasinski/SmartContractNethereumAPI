using Nethereum.ABI.FunctionEncoding.Attributes;

namespace ContractConfiguration.Models.MethodsOutputs
{
    [FunctionOutput]
    public class Ballot
    {
        [Parameter("bool", "isActive", 1)]
        public bool IsActive { get; set; }

        [Parameter("uint256", "candidatesSize", 2)]
        public int CandidatesSize { get; set; }
    }
}
