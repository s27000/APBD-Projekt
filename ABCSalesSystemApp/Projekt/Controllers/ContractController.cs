using Microsoft.AspNetCore.Mvc;
using Projekt.Models.Contract;
using Projekt.Services.Interfaces;

namespace Projekt.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private readonly IContractService _contractService;
        public ContractController(IContractService contractService)
        {
            _contractService = contractService;
        }

        [HttpPost("productContract")]
        public async Task<IActionResult> AddProductContract(ProductContractAddRequest productContractAddRequest, CancellationToken cancellationToken)
        {
            var newContractId = await _contractService.AddProductContract(productContractAddRequest, cancellationToken);
            return Ok("Successfully added a new product contract, with the id of: " + newContractId);
        }

        [HttpPost("subscriptionContract")]
        public async Task<IActionResult> AddSubscriptionContract(SubscriptionContractAddRequest subscriptionContractAddRequest, CancellationToken cancellationToken)
        {
            var tupleIds = await _contractService.AddSubscriptonContract(subscriptionContractAddRequest, cancellationToken);
            return Ok("Successfully added a new subscription contract, with the id of: " + tupleIds.Item1 +" and has already been payed for " +
                "(id of payment: " + tupleIds.Item2 + ") + " +
                ", A renewel contract has also been added with the id of:" + tupleIds.Item3);
        }
    }
}
