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
        public ContractRepository(AppDbContext appDbContext, IClientRepository clientRepository)
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

            var bestDiscount = await GetBestDiscount(productContractAddRequest.DateFrom, productContractAddRequest.DateTo, cancellationToken);

            if (await _context.ProductContracts
                    .Where(e => e.IdClient == productContractAddRequest.IdClient)
                    .AnyAsync(cancellationToken)
                && bestDiscount.Value < 5)
            {
                bestDiscount.IdDiscount = null;
                bestDiscount.Value = 5;
            }

            decimal totalPrice = Math.Round((product.AnnualPrice + productContractAddRequest.UpdateSupportExtension * 1000m)
                * ((100 - bestDiscount?.Value?? 0) / 100m), 2);

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
                DiscountValue = bestDiscount?.Value?? null,
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
                .FirstOrDefaultAsync(cancellationToken);

            if (productContract != null 
                && (productContract.DateFrom < productContractAddRequest.DateTo
                    && productContract.DateTo > productContractAddRequest.DateFrom)){
                throw new AlreadyPurchasedException("This client signed a product contract for this product in this timeframe");
            }

            var subscriptionsList = await _context.Subscriptions
                .Where(e => e.IdProduct == productContractAddRequest.IdProduct)
                .ToListAsync(cancellationToken);

            foreach(var subscription in subscriptionsList)
            {
                var subscriptionContract = await _context.SubscriptionContracts
                .Where(e => e.IdSubscription == subscription.IdSubscription && e.IdClient == productContractAddRequest.IdClient)
                .FirstOrDefaultAsync(cancellationToken);

                if(subscriptionContract != null
                    && (subscriptionContract.DateFrom < productContractAddRequest.DateTo
                    && subscriptionContract.DateTo > productContractAddRequest.DateFrom))
                {
                    throw new AlreadyPurchasedException("This client signed a subscription contract for this product in this timeframe");
                }
            }
        }
        private async Task<DiscountResponse> GetBestDiscount(DateTime DateFrom, DateTime DateTo, CancellationToken cancellationToken)
        {
            var bestAvailableDiscount = await _context.Discounts
                .Where(discount => discount.DateFrom < DateTo
                    && discount.DateTo > DateFrom)
                .OrderByDescending(discount => discount.Value)
                .FirstOrDefaultAsync(cancellationToken);

            var discountResponse = new DiscountResponse
            {
                IdDiscount = bestAvailableDiscount?.IdDiscount?? null,
                Value = bestAvailableDiscount?.Value ?? 0
            };

            return discountResponse;
        }

        public async Task<ProductContract> GetProductContract(int idProductContract, CancellationToken cancellationToken)
        {
            return await _context.ProductContracts
                .Where(e => e.IdProductContract == idProductContract).FirstOrDefaultAsync(cancellationToken) 
                ?? throw new NotFoundException("ProductContract does not exist");
        }

        public async Task<SubscriptionContract> GetSubscriptionContract(int idSubscriptionContract, CancellationToken cancellationToken)
        {
            return await _context.SubscriptionContracts
                .Where(e => e.IdSubscriptionContract == idSubscriptionContract).FirstOrDefaultAsync(cancellationToken)
                ?? throw new NotFoundException("SubscriptionContract does not exist");
        }


        public async Task<List<ProductContract>> GetProductContractsList(int idProduct, DateTime dateFrom, DateTime dateTo, CancellationToken cancellationToken)
        {
            return await _context.ProductContracts
                .Where(e => e.IdProduct == idProduct
                    && e.DateFrom < dateTo
                    && e.DateTo > dateFrom)
                .ToListAsync(cancellationToken);
        }

        public async Task<int> AddSubscriptionContract(SubscriptionContractAddRequest subscriptionContractAddRequest, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetClient(subscriptionContractAddRequest.IdClient, cancellationToken);

            var subscription = await _context.Subscriptions.Where(e => e.IdSubscription == subscriptionContractAddRequest.IdSubscription).FirstOrDefaultAsync(cancellationToken)
                 ?? throw new NotFoundException("Subscription does not exist");

            var subscriptionContractEndDate = subscriptionContractAddRequest.StartDate.AddMonths(subscription.SubscriptionDurationInMonths);

            var bestDiscount = await GetBestDiscount(subscriptionContractAddRequest.StartDate, subscriptionContractEndDate, cancellationToken);

            decimal totalPrice = Math.Round((subscription.MonthlyPrice * subscription.SubscriptionDurationInMonths)
                * ((100 - bestDiscount?.Value ?? 0) / 100m), 2);

            var newSubscriptionContract = new SubscriptionContract()
            {
                IdClient = subscriptionContractAddRequest.IdClient,
                IdSubscription = subscription.IdSubscription,
                DateFrom = subscriptionContractAddRequest.StartDate,
                DateTo = subscriptionContractEndDate,
                IdDiscount = bestDiscount?.IdDiscount ?? null,
                DiscountValue = bestDiscount?.Value ?? null,
                Price = totalPrice
            };
            await _context.SubscriptionContracts.AddAsync(newSubscriptionContract, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return newSubscriptionContract.IdSubscriptionContract;
        }

        public async Task<int> GenerateSubscriptionRenewelContract(int idSubscriptionContract, CancellationToken cancellationToken)
        {
            var subscriptionContract = await _context.SubscriptionContracts
                .Where(e => e.IdSubscriptionContract == idSubscriptionContract)
                .FirstOrDefaultAsync(cancellationToken);

            var subscription = await _context.Subscriptions
                .Where(e => e.IdSubscription == subscriptionContract.IdSubscription)
                .FirstOrDefaultAsync(cancellationToken);

            var newEndDateTo = subscriptionContract.DateTo.AddMonths(subscription.SubscriptionDurationInMonths);

            var subscriptionRenewelContract = new SubscriptionContract()
            {
                IdClient = subscriptionContract.IdClient,
                IdSubscription = subscriptionContract.IdSubscription,
                DateFrom = subscriptionContract.DateTo,
                DateTo = newEndDateTo,
                IdDiscount = null,
                DiscountValue = 5,
                Price = subscription.MonthlyPrice * subscription.SubscriptionDurationInMonths * 0.95m
            };

            await _context.SubscriptionContracts.AddAsync(subscriptionRenewelContract, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return subscriptionRenewelContract.IdSubscriptionContract;
        }
    }
}
