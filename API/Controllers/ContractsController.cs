using API.Models;
using Core.Components;
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

        //[HttpPost]
        //[Route("execute")]
        //public async Task<object> Execute([FromBody]ExecuteMethodData executeData)
        //{
        //    return await _facade.ExecuteMethod(executeData.ContractName, executeData.MethodName, executeData.Parameters);
        //}

        [HttpPost]
        [Route("getsimple")]
        public async Task<object> GetSimpleType([FromBody]ExecuteMethodData executeData)
        {
            return await _facade.InvokeGetSimpleType<string>(executeData.ContractName, executeData.MethodName, executeData?.Parameters);
        }

        [HttpPost]
        [Route("getcomplex")]
        public async Task<object> GetComplexType([FromBody]ExecuteMethodData executeData)
        {
            return await _facade.InvokeGetComplex(executeData.ContractName, executeData.MethodName, executeData?.Parameters);
        }
    }
}