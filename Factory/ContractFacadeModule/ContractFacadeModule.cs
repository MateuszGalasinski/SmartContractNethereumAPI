using Autofac;
using ContractManagement;
using ContractManagement.Components;

namespace Factory.ContractFacadeModule
{
    public class StoreRegisterModule : ContractMiddlewareRegisterModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ContractFacade>()
                .As<IContractFacade>()
                .SingleInstance();
        }
    }
}
