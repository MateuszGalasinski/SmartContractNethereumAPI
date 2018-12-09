using Autofac;
using ContractManagement;
using Core.Components;

namespace Factory.ContractManagement
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
