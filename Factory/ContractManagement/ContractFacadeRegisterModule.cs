using Autofac;
using ContractManagement;
using Core.Components;

namespace Factory.ContractManagement
{
    public class ContractFacadeRegisterModule : ContractMiddlewareRegisterModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ContractFacade>()
                .As<IContractFacade>()
                .SingleInstance();
        }
    }
}
