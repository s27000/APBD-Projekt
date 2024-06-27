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

        [HttpPost("create")]
        public async Task<IActionResult> AddProductContract(ProductContractAddRequest productContractAddRequest, CancellationToken cancellationToken)
        {
            var newContractId = await _contractService.AddProductContract(productContractAddRequest, cancellationToken);
            return Ok("Successfully added a new product contract, with the id of: " + newContractId);
        }
    }
}
