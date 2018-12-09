using Autofac;
using ContractManagement;
using Core.Components;

namespace Factory.ContractManagement
{
    public class StoreRegisterModule : ContractMiddlewareRegisterModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ContractStore>()
                .As<IContractStore>()
                .SingleInstance();
        }
    }
}
