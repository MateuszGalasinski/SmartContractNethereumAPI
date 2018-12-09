using API.Models.MethodsOutputs;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Core.Components;
using Core.Options;
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

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            ConfigureOptionsObjects(services);

            ContainerBuilder builder = ContainerCreator.BuildContainer();
            builder.Populate(services);
            Container = builder.Build();

            PrepareGetterStore();
            ReleaseContracts();

            return new AutofacServiceProvider(Container);
        }

        public void Configure(IApplicationBuilder app, IApplicationLifetime lifetime)
        {
            //app.UseHsts();

            app.UseHttpsRedirection();
            app.UseMvc();

            lifetime.ApplicationStopped.Register(() => Container.Dispose());
        }

        private void ConfigureOptionsObjects(IServiceCollection services)
        {
            services.Configure<NetworkOptions>(Configuration.GetSection("network_options"));
            services.Configure<AccountOptions>(Configuration.GetSection("user_account"));
        }

        private void ReleaseContracts()
        {
            var contractProvider = Container.Resolve<IContractDefinitionProvider>();
            contractProvider.ReadAllContracts(Configuration.GetValue<string>("contracts_dir"));

            var contractFacade = Container.Resolve<IContractFacade>();
            contractFacade.ReleaseAllContracts().Wait();
        }

        private void PrepareGetterStore()
        {
            var store = Container.Resolve<IGetterStore>();
            store.Add<Ballot>("MyStringStore", "ballots");
        }
    }
}
