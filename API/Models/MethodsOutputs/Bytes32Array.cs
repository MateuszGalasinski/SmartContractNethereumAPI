using Nethereum.ABI.FunctionEncoding.Attributes;
using System.Collections.Generic;

namespace API.Models.MethodsOutputs
{
    [FunctionOutput]
    public class Bytes32Array
    {
        [Parameter("bytes32", "", 1)]
        public List<string> Data { get; set; }
    }
}
