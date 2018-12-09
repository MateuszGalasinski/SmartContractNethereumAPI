using Core.Components;
using Core.Exceptions;
using Nethereum.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractManagement
{
    public class GetterStore : IGetterStore
    {
        private readonly Dictionary<string, Func<Function, object[], Task<object>>> _methodsDictionary = new Dictionary<string, Func<Function, object[], Task<object>>>();

        public void Add<TReturn>(string contractName, string contractMethod) where TReturn : new()
        {
            _methodsDictionary.Add(CreateId(contractName, contractMethod), async (function, parameters) => await function.CallDeserializingToObjectAsync<TReturn>(parameters));
        }

        public Task<object> InvokeGetComplex(string contractName, string contractMethod, Function function, Dictionary<string, object> parameters)
        {
            if (!_methodsDictionary.ContainsKey(CreateId(contractName, contractMethod)))
            {
                throw new ContractException($"Method: {contractMethod} in contract named: {contractName} does not exists in methods store.");
            }
            return _methodsDictionary[CreateId(contractName, contractMethod)](function, parameters.Values.ToArray());
        }

        private string CreateId(string contractName, string contractMethod) => $"{contractName}:{contractMethod}";
    }
}
