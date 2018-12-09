using Autofac;
using ContractManagement;
using Core.Components;

namespace Factory.ContractManagement
{
    public class GetterStoreModule : ContractMiddlewareRegisterModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GetterStore>()
                .As<IGetterStore>()
                .SingleInstance();
        }
    }
}
