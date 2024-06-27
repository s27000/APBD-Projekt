﻿using Projekt.Models.Contract;
using Projekt.Models.Domain;

namespace Projekt.Repositories.Interfaces
{
    public interface IContractRepository
    {
        Task<int> AddProductContract(ProductContractAddRequest productContractAddRequest, int updateSupportDuration, CancellationToken cancellationToken);
        Task<ProductContract> GetProductContract(int idProductContract, CancellationToken cancellationToken);
        Task<List<ProductContract>> GetProductContractsList(int idProduct, DateTime dateFrom, DateTime dateTo, CancellationToken cancellationToken);
    }
}
