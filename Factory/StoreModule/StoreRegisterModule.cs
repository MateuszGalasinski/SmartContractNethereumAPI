using Autofac;
using ContractManagement;
using ContractManagement.Components;

namespace Factory.StoreModule
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
