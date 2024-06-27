using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Projekt.Models.Payment.Request;
using Projekt.Services.Interfaces;

namespace Projekt.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("productContractPayment")]
        public async Task<IActionResult> AddProductContractPayment(ProductContractPaymentRequest productContractPaymentRequest, 
            CancellationToken cancellationToken)
        {
            var newPaymentId = await _paymentService.AddProductContractPayment(productContractPaymentRequest, cancellationToken);
            return Ok("Successfully added a new product contract payment, with the id of: " + newPaymentId);
        }
        [HttpPost("subscriptionContractPayment")]
        public async Task<IActionResult> AddSubscriptionContractPayment(SubscriptionContractPaymentRequest subscriptionContractPaymentRequest,
            CancellationToken cancellationToken)
        {
            var tupleIds = await _paymentService.AddSubscriptionContractPayment(subscriptionContractPaymentRequest, cancellationToken);
            return Ok("Successfully added a new subscription contract payment, with the id of: " + tupleIds.Item1 +
                ", a new renewel contract has been created with the id of: " + tupleIds.Item2);
        }

        [HttpGet("totalRealIncome")]
        public async Task<IActionResult> GetTotalRealIncome([FromQuery] TotalIncomeRequest totalIncomeRequest, [FromQuery] int idProduct, [FromQuery] string? currency, CancellationToken cancellationToken)
        {
            if (idProduct == 0)
            {
                var totalRealIncome = await _paymentService.GetTotalRealIncome(totalIncomeRequest, currency, cancellationToken);
                return Ok(totalRealIncome);
            }
            else
            {
                var productRealIncome = await _paymentService.GetProductRealIncome(totalIncomeRequest, idProduct, currency, cancellationToken);
                return Ok(new { Currency = currency?? "PLN", ProductRealIncome = productRealIncome });
            }
        }

        [HttpGet("totalPredictedIncome")]
        public async Task<IActionResult> GetTotalPredictedIncome([FromQuery] TotalIncomeRequest totalIncomeRequest, [FromQuery] int idProduct, [FromQuery] string? currency, CancellationToken cancellationToken)
        {
            if(idProduct == 0)
            {
                var totalPredictedIncome = await _paymentService.GetTotalPredictedIncome(totalIncomeRequest, currency, cancellationToken);
                return Ok(totalPredictedIncome);
            }
            else
            {
                var productPredictedIncome = await _paymentService.GetProductPredictedIncome(totalIncomeRequest, idProduct, currency, cancellationToken);
                return Ok(new { Currency = currency ?? "PLN", ProductPredictedIncome = productPredictedIncome });
            }
        }
    }
}
