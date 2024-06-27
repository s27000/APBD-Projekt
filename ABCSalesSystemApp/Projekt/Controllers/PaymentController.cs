using Microsoft.AspNetCore.Mvc;
using Projekt.Models.Payment;
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
    }
}
