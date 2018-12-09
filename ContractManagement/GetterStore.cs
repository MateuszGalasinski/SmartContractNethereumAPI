using Core.Components;
using Core.Exceptions;
using Nethereum.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContractManagement
{
    public class GetterStore : IGetterStore
    {
        private readonly Dictionary<string, Func<Function, object[], object>> _methodsDictionary = new Dictionary<string, Func<Function, object[], object>>();

        public void Add<TReturn>(string contractName, string contractMethod) where TReturn : new()
        {
            _methodsDictionary.Add(CreateId(contractName, contractMethod), (function, parameters) => await function.CallDeserializingToObjectAsync<TReturn>(parameters));
        }

        public object InvokeGetComplex(string contractName, string contractMethod, Function function, Dictionary<string, object> parameters)
        {
            if (!_methodsDictionary.ContainsKey(CreateId(contractName, contractMethod)))
            {
                throw new ContractException($"Method: {contractMethod} in contract named: {contractName} does not exists in methods store.");
            }
            var result = _methodsDictionary[CreateId(contractName, contractMethod)](function, parameters.Values.ToArray());
            return result;
        }

        private string CreateId(string contractName, string contractMethod) => $"{contractName}:{contractMethod}";
    }
}
