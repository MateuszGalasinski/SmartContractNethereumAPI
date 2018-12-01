using Autofac;
using ContractManagement;
using ContractManagement.Components;

namespace Factory.AccountProviderModule
{
    public class AccountProviderModule : ContractMiddlewareRegisterModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AccountProvider>()
                .As<IAccountProvider>()
                .SingleInstance();
        }
    }
}
