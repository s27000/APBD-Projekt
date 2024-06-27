using Projekt.Models.Payment;

namespace Projekt.Repositories.Interfaces
{
    public interface IPaymentRepository
    {
        Task<int> AddProductContractPayment(ProductContractPaymentRequest productContractPaymentRequest, CancellationToken cancellationToken);
    }
}
