using Projekt.Models.Payment.Request;
using Projekt.Models.Payment.Responce;

namespace Projekt.Repositories.Interfaces
{
    public interface IPaymentRepository
    {
        Task<int> AddProductContractPayment(ProductContractPaymentRequest productContractPaymentRequest, CancellationToken cancellationToken);
        Task<ProductIncomeResponse> GetProductPredictedIncome(TotalIncomeRequest totalIncomeRequest, int idProduct, CancellationToken cancellationToken);
        Task<ProductIncomeResponse> GetProductRealIncome(TotalIncomeRequest totalIncomeRequest, int idProduct, CancellationToken cancellationToken);
        Task<TotalIncomeResponse> GetTotalPredictedIncome(TotalIncomeRequest totalIncomeRequest, CancellationToken cancellationToken);
        Task<TotalIncomeResponse> GetTotalRealIncome(TotalIncomeRequest totalIncomeRequest, CancellationToken cancellationToken);
    }
}
