using Autofac;
using Core.Components;
using Logging;

namespace Factory.Logging
{
    public class LoggingModule : ContractMiddlewareRegisterModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TraceLogger>()
                .As<ILogger>()
                .SingleInstance();
        }
    }
}
