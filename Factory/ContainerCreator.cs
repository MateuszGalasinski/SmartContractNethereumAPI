using Autofac;
using System.Reflection;

namespace Factory
{
    public static class ContainerCreator
    {
        public static ContainerBuilder BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyModules<ContractMiddlewareRegisterModule>(Assembly.GetExecutingAssembly());

            return builder;
        }
    }
}
