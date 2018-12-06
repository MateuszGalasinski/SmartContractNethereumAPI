using ContractManagement.Components;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class ContractsController : Controller
    {
        private readonly IContractFacade _facade;

        public ContractsController(IContractFacade facade)
        {
            _facade = facade;
        }

        [HttpGet]
        [Route("getBalance/{walletAddress}")]
        public async Task<decimal> GetBalance([FromRoute]string walletAddress)
        {
            return await _facade.GetBalance(walletAddress);
        }

        [HttpGet]
        [Route("list")]
        public async Task<string[]> GetAll()
        {
            return await _facade.GetAllContractsNames();
        }

        [HttpGet]
        [Route("checkContract/{name}")]
        public async Task<bool> CheckContract([FromRoute] string name)
        {
            return await _facade.TryGetContractAddress(name) != null;
        }

        [HttpGet]
        [Route("exeContract/{name}/{contractMethod}/{value}")]
        public async Task<string> ExecuteContract([FromRoute] string name, [FromRoute] string contractMethod, [FromRoute] int value)
        {
            return await ExecuteMethod(name, contractMethod, value);
        }

        private async Task<string> ExecuteMethod(string name, string contractMethod, int value)
        {
            return await _facade.ExecuteMethod(name, contractMethod, value);
        }
    }
}