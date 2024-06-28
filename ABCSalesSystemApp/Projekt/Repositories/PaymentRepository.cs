using Microsoft.EntityFrameworkCore;
using Projekt.Context;
using Projekt.Exceptions;
using Projekt.Models.Domain;
using Projekt.Models.Payment.Request;
using Projekt.Models.Payment.Responce;
using Projekt.Repositories.Interfaces;
using System.Threading;

namespace Projekt.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppDbContext _context;
        private readonly IContractRepository _contractRepository;
        public PaymentRepository(AppDbContext context, IContractRepository contractRepository)
        {
            _context = context;
            _contractRepository = contractRepository;
        }

        public async Task<int> AddProductContractPayment(ProductContractPaymentRequest productContractPaymentRequest, CancellationToken cancellationToken)
        {
            var productContract = await _contractRepository.GetProductContract(productContractPaymentRequest.IdProductContract, cancellationToken);

            PaymentDateIsValid(productContractPaymentRequest.Date, productContract.DateFrom, productContract.DateTo);
            await ProductContractPaymentIsNotFulfilledOrOverflown(productContract, productContractPaymentRequest.PaymentValue, cancellationToken);

            var newProductContractPayment = new ProductContractPayment()
            {
                IdProductContract = productContract.IdProductContract,
                Description = productContractPaymentRequest.Description,
                Date = productContractPaymentRequest.Date,
                PaymentValue = productContractPaymentRequest.PaymentValue
            };

            await _context.ProductContractPayments.AddAsync(newProductContractPayment, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return newProductContractPayment.IdProductContractPayment;
        }

        public async Task<int> AddSubscriptionContractPayment(SubscriptionContractPaymentRequest subscriptionContractPaymentRequest, CancellationToken cancellationToken)
        {
            var subscriptionContract = await _contractRepository.GetSubscriptionContract(subscriptionContractPaymentRequest.IdSubscriptionContract, cancellationToken);
            PaymentDateIsValid(subscriptionContractPaymentRequest.Date, subscriptionContract.DateFrom, subscriptionContract.DateTo);

            bool isPaid = await SubscriptionContractIsPaidFor(subscriptionContract, cancellationToken);
            if (isPaid)
            {
                throw new AlreadyPurchasedException("This Contract has already been fulfilled");
            }

            SubscriptionPaymentIsCorrect(subscriptionContract, subscriptionContractPaymentRequest.PaymentValue, cancellationToken);

            var newSubscriptionContractPayment = new SubscriptionContractPayment()
            {
                IdSubscriptionContract = subscriptionContract.IdSubscriptionContract,
                Description = subscriptionContractPaymentRequest.Description,
                Date = subscriptionContractPaymentRequest.Date,
                PaymentValue = subscriptionContractPaymentRequest.PaymentValue
            };

            await _context.SubscriptionContractPayments.AddAsync(newSubscriptionContractPayment, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return newSubscriptionContractPayment.IdSubscriptionContract;
        }

        public async Task<ProductIncomeResponse> GetProductPredictedIncome(TotalIncomeRequest totalIncomeRequest, int idProduct, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .Where(e => e.IdProduct == idProduct)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new NotFoundException("Product does not exist");

            var subscriptionList = await _context.Subscriptions
                .Where(e => e.IdProduct == idProduct)
                .ToListAsync(cancellationToken);

            decimal contractPredictedIncome = 0m;
            decimal subscriptionPredictedIncome = 0m;

            var productContractsList = await _contractRepository.GetProductContractsList(product.IdProduct, totalIncomeRequest.DateFrom, totalIncomeRequest.DateTo, cancellationToken);

            foreach (var productContract in productContractsList)
            {
                var totalContractPayment = await GetTotalProductContractPayment(productContract, cancellationToken);
                contractPredictedIncome += productContract.TotalPrice;
            }

            foreach (var subscription in subscriptionList)
            {
                var subscriptionContractsList = await _contractRepository.GetSubscriptionContractsList(subscription.IdSubscription, totalIncomeRequest.DateFrom, totalIncomeRequest.DateTo, cancellationToken);
                foreach (var subscriptionContract in subscriptionContractsList)
                {
                    subscriptionPredictedIncome += subscriptionContract.Price;
                }
            }

            var productResponce = new ProductIncomeResponse()
            {
                Name = product.Name,
                Description = product.Description,
                ProductCategory = product.ProductCategory.ToString(),
                ContractIncome = contractPredictedIncome,
                SubscriptionIncome = subscriptionPredictedIncome,
                TotalProductIncome = contractPredictedIncome + subscriptionPredictedIncome
            };

            return productResponce;
        }

        public async Task<ProductIncomeResponse> GetProductRealIncome(TotalIncomeRequest totalIncomeRequest, int idProduct, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .Where(e => e.IdProduct == idProduct)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new NotFoundException("Product does not exist");

            var subscriptionList = await _context.Subscriptions
                .Where(e => e.IdProduct == idProduct)
                .ToListAsync(cancellationToken);

            decimal contractRealIncome = 0m;
            decimal subscriptionRealIncome = 0m;

            var productContractsList = await _contractRepository.GetProductContractsList(product.IdProduct, totalIncomeRequest.DateFrom, totalIncomeRequest.DateTo, cancellationToken);

            foreach (var productContract in productContractsList)
            {
                var totalContractPayment = await GetTotalProductContractPayment(productContract, cancellationToken);
                if (totalContractPayment == productContract.TotalPrice)
                {
                    contractRealIncome += productContract.TotalPrice;
                }
            }

            foreach (var subscription in subscriptionList)
            {
                var subscriptionContractsList = await _contractRepository.GetSubscriptionContractsList(subscription.IdSubscription, totalIncomeRequest.DateFrom, totalIncomeRequest.DateTo, cancellationToken);
                foreach(var subscriptionContract in subscriptionContractsList)
                {
                    if(await SubscriptionContractIsPaidFor(subscriptionContract, cancellationToken))
                    {
                        subscriptionRealIncome += subscriptionContract.Price;
                    }
                }
            }

            var productResponce = new ProductIncomeResponse()
            {
                Name = product.Name,
                Description = product.Description,
                ProductCategory = product.ProductCategory.ToString(),
                ContractIncome = contractRealIncome,
                SubscriptionIncome = subscriptionRealIncome,
                TotalProductIncome = contractRealIncome + subscriptionRealIncome
            };

            return productResponce;
        }

        public async Task<TotalIncomeResponse> GetTotalPredictedIncome(TotalIncomeRequest totalIncomeRequest, CancellationToken cancellationToken)
        {
            var products = await _context.Products.ToListAsync(cancellationToken);

            var productIncomeResponceList = new List<ProductIncomeResponse>();

            var totalContractPredictedIncome = 0m;
            var totalSubscriptonPredictedIncome = 0m;
            var totalPredictedIncome = 0m;

            foreach (var product in products)
            {
                var productResponce = await GetProductPredictedIncome(totalIncomeRequest, product.IdProduct, cancellationToken);

                totalContractPredictedIncome += productResponce.ContractIncome;
                totalSubscriptonPredictedIncome += productResponce.SubscriptionIncome;
                totalPredictedIncome += productResponce.TotalProductIncome;
                productIncomeResponceList.Add(productResponce);
            }

            return new TotalIncomeResponse()
            {
                Products = productIncomeResponceList,
                TotalContractIncome = totalContractPredictedIncome,
                TotalSubscriptionIncome = totalSubscriptonPredictedIncome,
                TotalIncome = totalPredictedIncome
            };
        }

        public async Task<TotalIncomeResponse> GetTotalRealIncome(TotalIncomeRequest totalIncomeRequest, CancellationToken cancellationToken)
        {
            var products = await _context.Products.ToListAsync(cancellationToken);

            var productIncomeResponceList = new List<ProductIncomeResponse>();

            var totalContractRealIncome = 0m;
            var totalSubscriptonRealIncome = 0m;
            var totalRealIncome = 0m;

            foreach (var product in products)
            {
                var productResponce = await GetProductRealIncome(totalIncomeRequest, product.IdProduct, cancellationToken);

                totalContractRealIncome += productResponce.ContractIncome;
                totalSubscriptonRealIncome += productResponce.SubscriptionIncome;
                totalRealIncome += productResponce.TotalProductIncome;

                productIncomeResponceList.Add(productResponce);
            }

            return new TotalIncomeResponse()
            {
                Products = productIncomeResponceList,
                TotalContractIncome = totalContractRealIncome,
                TotalSubscriptionIncome = totalSubscriptonRealIncome,
                TotalIncome = totalRealIncome
            };
        }

        private async Task<decimal> GetTotalProductContractPayment(ProductContract productContract, CancellationToken cancellationToken)
        {
            return await _context.ProductContractPayments
                .Where(e => e.IdProductContract == productContract.IdProductContract)
                .SumAsync(e => e.PaymentValue, cancellationToken);
        }

        private async Task<bool> SubscriptionContractIsPaidFor(SubscriptionContract subscriptionContract, CancellationToken cancellationToken)
        {
            var isAlreadyPurchased = await _context.SubscriptionContractPayments
                .Where(e => e.IdSubscriptionContract == subscriptionContract.IdSubscriptionContract)
                .AnyAsync(cancellationToken);

            if (isAlreadyPurchased)
            {
                return true;
            }
            return false;
        }

        private void SubscriptionPaymentIsCorrect(SubscriptionContract subscriptionContract, decimal paymentValue, CancellationToken cancellationToken)
        {
            if (subscriptionContract.Price != paymentValue)
            {
                throw new ArgumentException("The Payment for this subscription is incorrect");
            }
        }

        private void PaymentDateIsValid(DateTime paymentDate, DateTime contractDateFrom, DateTime contractDateTo)
        {
            if(paymentDate > contractDateTo || paymentDate < contractDateFrom)
            {
                throw new ArgumentException("The PaymentDate is either overdue, or too early");
            }
        }

        private async Task ProductContractPaymentIsNotFulfilledOrOverflown(ProductContract productContract, decimal paymentValue, CancellationToken cancellationToken)
        {
            var totalContractPayment = await GetTotalProductContractPayment(productContract, cancellationToken);
            
            if (totalContractPayment == productContract.TotalPrice)
            {
                throw new AlreadyPurchasedException("This Contract has already been fulfilled");
            }
            if(totalContractPayment + paymentValue > productContract.TotalPrice)
            {
                throw new ArgumentException("The total payment for this contract is beyond total price");
            }
        }
    }
}
