using Projekt.Models.Contract;
using Projekt.Models.Domain;

namespace Projekt.Repositories.Interfaces
{
    public interface IContractRepository
    {
        Task<int> AddProductContract(ProductContractAddRequest productContractAddRequest, int updateSupportDuration, CancellationToken cancellationToken);
        Task<int> AddSubscriptionContract(SubscriptionContractAddRequest subscriptionContractAddRequest, CancellationToken cancellationToken);
        Task<int> GenerateSubscriptionRenewelContract(int idSubscriptionContract, CancellationToken cancellationToken);
        Task<ProductContract> GetProductContract(int idProductContract, CancellationToken cancellationToken);
        Task<SubscriptionContract> GetSubscriptionContract(int idSubscriptionContract, CancellationToken cancellationToken);
        Task<List<ProductContract>> GetProductContractsList(int idProduct, DateTime dateFrom, DateTime dateTo, CancellationToken cancellationToken);
        Task<List<SubscriptionContract>> GetSubscriptionContractsList(int idSubscription, DateTime dateFrom, DateTime dateTo, CancellationToken cancellationToken);
    }
}
