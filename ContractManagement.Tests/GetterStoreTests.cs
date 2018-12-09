using NUnit.Framework;

namespace ContractManagement.Tests
{
    [TestFixture]
    public class GetterStoreTests
    {
        [Test]
        public void AddFunctionTest()
        {
            // outdated

            //IGetterStore store = new GetterStore();
            //store.Add<ContractDefinitionProvider>("Cymande", "Dove", TestGetter);
            //var result = store.InvokeGetComplex("Cymande", "Dove", new Dictionary<string, object>() { });
            //result.Should().BeOfType<ContractDefinitionProvider>();
        }

        private ContractDefinitionProvider TestGetter(object[] parameters) => new ContractDefinitionProvider();
    }
}
