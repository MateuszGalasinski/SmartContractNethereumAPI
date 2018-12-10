using API.Models.MethodsOutputs;
using Autofac;
using Core.Components;
using Microsoft.Extensions.Configuration;
using System.Management.Automation;

namespace API
{
    public static class ContractsStartup
    {
        public static void InitContractsManagement(IContainer container, IConfiguration configuration)
        {
            ReleaseContracts(container, configuration);
            PrepareGetterStore(container);
        }

        private static void ReleaseContracts(IContainer container, IConfiguration configuration)
        {
            var contractProvider = container.Resolve<IContractDefinitionProvider>();
            contractProvider.ReadAllContracts(configuration.GetValue<string>("contracts_dir"));

            var contractFacade = container.Resolve<IContractFacade>();
            contractFacade.ReleaseAllContracts().Wait();
        }

        private static void PrepareGetterStore(IContainer container)
        {
            var store = container.Resolve<IGetterStore>();
            store.Add<Ballot>("VoteManager", "ballots");
            store.Add<Bytes32Array>("VoteManager", "getCandidateNamesForBallot");
        }

        private static void StartTestBlockchain(IContainer container)
        {
            var powershell = container.Resolve<PowerShell>();
            powershell.Commands.AddScript(
                "ganache-cli --account=\"0x59bb027fa0ea6e6a979f0f26d774f396d5b3e4704f052acaab1497666d5ed283,3000000000000000000000\" -b 3");
            powershell.Invoke();
            System.Threading.Thread.Sleep(2000);
        }
    }
}
