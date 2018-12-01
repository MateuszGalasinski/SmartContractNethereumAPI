using ContractManagement.Components;
using ContractManagement.Models;
using Microsoft.Extensions.Options;

namespace ContractManagement
{
    public class AccountProvider : IAccountProvider
    {
        private readonly Account _account;

        public AccountProvider(IOptions<Account> account)
        {
            _account = account.Value;
        }

        public Account GetAccount()
        {
            return _account;
        }
    }
}
