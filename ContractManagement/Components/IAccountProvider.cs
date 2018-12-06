using ContractManagement.Models.Options;

namespace ContractManagement.Components
{
    public interface IAccountProvider
    {
        AccountOptions GetAccount();
    }
}