using ContractManagement.Models;

namespace ContractManagement.Components
{
    public interface IAccountProvider
    {
        Account GetAccount();
    }
}