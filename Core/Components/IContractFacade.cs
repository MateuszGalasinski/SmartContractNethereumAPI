using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Components
{
    public interface IContractFacade
    {
        Task ReleaseAllContracts();
        Task<string[]> GetAllContractsNames();
        Task<string> TryGetContractAddress(string name);
        Task<object> ExecuteMethod(string name, string contractMethod, Dictionary<string, object> parameters);
        Task<decimal> GetBalance(string address);

        Task<object> InvokeGetComplex(string name, string contractMethod, Dictionary<string, object> parameters);

        Task<TReturn> InvokeGetSimpleType<TReturn>(string name, string contractMethod,
            Dictionary<string, object> parameters);
    }
}
