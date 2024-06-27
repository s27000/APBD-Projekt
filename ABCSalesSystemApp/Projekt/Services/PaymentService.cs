using Projekt.Models.Payment;
using Projekt.Repositories.Interfaces;
using Projekt.Services.Interfaces;

namespace Projekt.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<int> AddProductContractPayment(ProductContractPaymentRequest productContractPaymentRequest, CancellationToken cancellationToken)
        {
            productContractPaymentRequest.VerifyBody();
            return await _paymentRepository.AddProductContractPayment(productContractPaymentRequest, cancellationToken);
        }
    }
}
