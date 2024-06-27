using Projekt.Models.Contract;

namespace Projekt.Services.Interfaces
{
    public interface IContractService
    {
        Task<int> AddProductContract(ProductContractAddRequest productContractAddRequest, CancellationToken cancellationToken);
    }
}
