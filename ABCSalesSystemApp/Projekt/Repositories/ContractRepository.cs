using Microsoft.EntityFrameworkCore;
using Projekt.Context;
using Projekt.Exceptions;
using Projekt.Models.Contract;
using Projekt.Models.Domain;
using Projekt.Repositories.Interfaces;
using System.Threading;

namespace Projekt.Repositories
{
    public class ContractRepository : IContractRepository
    {
        private readonly AppDbContext _context;
        private readonly IClientRepository _clientRepository;
        public ContractRepository(IClientRepository clientRepository, AppDbContext appDbContext)
        {
            _context = appDbContext;
            _clientRepository = clientRepository;
        }

        public async Task<int> AddProductContract(ProductContractAddRequest productContractAddRequest, int updateSupportDuration, CancellationToken cancellationToken)
        {
            await HasNoActiveContract(productContractAddRequest, cancellationToken);

            var client = await _clientRepository.GetClient(productContractAddRequest.IdClient, cancellationToken);

            var product = await _context.Products.Where(e => e.IdProduct == productContractAddRequest.IdProduct).FirstOrDefaultAsync(cancellationToken)
                 ?? throw new NotFoundException("Product does not exist");

            var bestDiscount = await _context.Discounts
                .Where(discount => discount.DateFrom < productContractAddRequest.DateTo
                && discount.DateTo > productContractAddRequest.DateFrom)
                .OrderByDescending(discount => discount.Value)
                .FirstOrDefaultAsync(cancellationToken);

            var bestDiscountValue = await GetBestDiscountValue(productContractAddRequest, cancellationToken);

            decimal totalPrice = Math.Round((product.AnnualPrice + productContractAddRequest.UpdateSupportExtension * 1000m)
                * ((100 - bestDiscountValue) / 100m), 2);

            var newProductContract = new ProductContract()
            {
                IdClient = client.IdClient,
                IdProduct = product.IdProduct,
                ProductVersion = product.Version,
                DateFrom = productContractAddRequest.DateFrom,
                DateTo = productContractAddRequest.DateTo,
                ProductUpdateDescription = productContractAddRequest.ProductUpdateDescription,
                UpdateSupportDuration = updateSupportDuration,
                IdDiscount = bestDiscount?.IdDiscount?? null,
                Value = bestDiscountValue,
                TotalPrice = totalPrice
            };

            await _context.ProductContracts.AddAsync(newProductContract);
            await _context.SaveChangesAsync();

            return newProductContract.IdProductContract;
        }

        private async Task HasNoActiveContract(ProductContractAddRequest productContractAddRequest, CancellationToken cancellationToken)
        {
            var productContract = await _context.ProductContracts
                .Where(e => e.IdClient == productContractAddRequest.IdClient && e.IdProduct == productContractAddRequest.IdProduct)
                .FirstOrDefaultAsync();

            if (productContract != null 
                && (productContract.DateFrom < productContractAddRequest.DateTo
                    && productContract.DateTo > productContractAddRequest.DateFrom)){
                throw new AlreadyPurchasedException("This client signed a contract for this product");
            }
        }

        private async Task<int> GetBestDiscountValue(ProductContractAddRequest productContractAddRequest, CancellationToken cancellationToken)
        {
            var bestAvailableDiscount = await _context.Discounts
                .Where(discount => discount.DateFrom < productContractAddRequest.DateTo
                && discount.DateTo > productContractAddRequest.DateFrom)
                .OrderByDescending(discount => discount.Value)
                .FirstOrDefaultAsync(cancellationToken);

            var bestDiscountValue = bestAvailableDiscount?.Value ?? 0;

            if (await _context.ProductContracts
                .Where(e => e.IdClient == productContractAddRequest.IdClient)
                .AnyAsync(cancellationToken)
                && bestDiscountValue < 5)
            {
                bestDiscountValue = 5;
            }

            return bestDiscountValue;
        }

        public async Task<ProductContract> GetProductContract(int idProductContract, CancellationToken cancellationToken)
        {
            return await _context.ProductContracts
                .Where(e => e.IdProductContract == idProductContract).FirstOrDefaultAsync(cancellationToken) 
                ?? throw new NotFoundException("ProductContract does not exist");
        }
    }
}
