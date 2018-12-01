using Autofac;
using ContractManagement;
using ContractManagement.Components;

namespace Factory.ProviderModule
{
    public class ContractProviderModule : ContractMiddlewareRegisterModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ContractDefinitionProvider>()
                .As<IContractDefinitionProvider>()
                .SingleInstance();
        }
    }
}
