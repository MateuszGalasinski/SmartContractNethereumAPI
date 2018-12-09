using Autofac;
using ContractManagement;
using Core.Components;

namespace Factory.ContractManagement
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
