using Projekt.Models.Payment;
using Projekt.Repositories.Interfaces;

namespace Projekt.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<int> AddProductContractPayment(ProductContractPaymentRequest productContractPaymentRequest, CancellationToken cancellationToken);
    }
}
