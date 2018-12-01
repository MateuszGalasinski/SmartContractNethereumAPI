using Autofac;
using Autofac.Extensions.DependencyInjection;
using ContractManagement.Components;
using Factory;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IContainer Container { get; private set; }
        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            ContainerBuilder builder = ContainerCreator.BuildContainer();
            builder.Populate(services);
            Container = builder.Build();

            ReleaseContracts();

            return new AutofacServiceProvider(Container);
        }

        private void ReleaseContracts()
        {
            var contractProvider = Container.Resolve<IContractDefinitionProvider>();
            contractProvider.ReadContractFromFile(Configuration.GetValue<string>("ContractFilePath"));

            var contractFacade = Container.Resolve<IContractFacade>();
            contractFacade.ReleaseAllContracts();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IApplicationLifetime lifetime)
        {
            //app.UseHsts();

            app.UseHttpsRedirection();
            app.UseMvc();

            lifetime.ApplicationStopped.Register(() => Container.Dispose());
        }
    }
}
