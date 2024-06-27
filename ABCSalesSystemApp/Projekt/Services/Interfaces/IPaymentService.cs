using Projekt.Models.Payment.Request;
using Projekt.Models.Payment.Responce;
using Projekt.Repositories.Interfaces;

namespace Projekt.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<int> AddProductContractPayment(ProductContractPaymentRequest productContractPaymentRequest, CancellationToken cancellationToken);
        Task<Tuple<int, int>> AddSubscriptionContractPayment(SubscriptionContractPaymentRequest subscriptionContractPaymentRequest, CancellationToken cancellationToken);
        Task<ProductIncomeResponse> GetProductPredictedIncome(TotalIncomeRequest totalIncomeRequest, int idProduct, string? currency, CancellationToken cancellationToken);
        Task<ProductIncomeResponse> GetProductRealIncome(TotalIncomeRequest totalIncomeRequest, int idProduct, string? currency, CancellationToken cancellationToken);
        Task<TotalIncomeResponse> GetTotalPredictedIncome(TotalIncomeRequest totalIncomeRequest, string? currency, CancellationToken cancellationToken);
        Task<TotalIncomeResponse> GetTotalRealIncome(TotalIncomeRequest totalIncomeRequest, string? currency, CancellationToken cancellationToken);
    }
}
