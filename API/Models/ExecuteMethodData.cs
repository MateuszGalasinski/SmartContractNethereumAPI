using System.Collections.Generic;

namespace API.Models
{
    public class ExecuteMethodData
    {
        public string ContractName { get; set; }
        public string MethodName { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
    }
}
