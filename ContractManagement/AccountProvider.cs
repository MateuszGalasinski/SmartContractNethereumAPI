using Core.Components;
using Core.Options;
using Microsoft.Extensions.Options;

namespace ContractManagement
{
    public class AccountProvider : IAccountProvider
    {
        private readonly AccountOptions _account;

        public AccountProvider(IOptionsMonitor<AccountOptions> account)
        {
            _account = account.CurrentValue;
        }

        public AccountOptions GetAccount()
        {
            return _account;
        }
    }
}
