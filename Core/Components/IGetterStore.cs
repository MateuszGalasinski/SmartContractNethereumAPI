using Nethereum.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Components
{
    public interface IGetterStore
    {
        void Add<TReturn>(string contractName, string contractMethod) where TReturn : new();

        Task<object> InvokeGetComplex(string contractName,
            string contractMethod,
            Function function,
            Dictionary<string, object> parameters);
    }
}
