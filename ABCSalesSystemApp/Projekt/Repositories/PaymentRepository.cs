using Microsoft.EntityFrameworkCore;
using Projekt.Context;
using Projekt.Exceptions;
using Projekt.Migrations;
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
                IdProductContract = productContractPaymentRequest.IdProductContract,
                Description = productContractPaymentRequest.Description,
                Date = productContractPaymentRequest.Date,
                PaymentValue = productContractPaymentRequest.PaymentValue
            };

            await _context.ProductContractPayments.AddAsync(newProductContractPayment, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return newProductContractPayment.IdProductContractPayment;
        }

        public async Task<ProductIncomeResponse> GetProductPredictedIncome(TotalIncomeRequest totalIncomeRequest, int idProduct, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .Where(e => e.IdProduct == idProduct)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new NotFoundException("Product does not exist"); ;

            decimal totalProductPredictedIncome = 0m;
            var productContractsList = await _contractRepository.GetProductContractsList(product.IdProduct, totalIncomeRequest.DateFrom, totalIncomeRequest.DateTo, cancellationToken);

            foreach (var productContract in productContractsList)
            {
                var totalContractPayment = await GetTotalContractPayment(productContract, cancellationToken);
                totalProductPredictedIncome += productContract.TotalPrice;
            }

            var productResponce = new ProductIncomeResponse()
            {
                Name = product.Name,
                Description = product.Description,
                ProductCategory = product.ProductCategory.ToString(),
                TotalIncome = totalProductPredictedIncome
            };

            return productResponce;
        }

        public async Task<ProductIncomeResponse> GetProductRealIncome(TotalIncomeRequest totalIncomeRequest, int idProduct, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .Where(e => e.IdProduct == idProduct)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new NotFoundException("Product does not exist"); ;

            decimal totalProductRealIncome = 0m;
            var productContractsList = await _contractRepository.GetProductContractsList(product.IdProduct, totalIncomeRequest.DateFrom, totalIncomeRequest.DateTo, cancellationToken);

            foreach (var productContract in productContractsList)
            {
                var totalContractPayment = await GetTotalContractPayment(productContract, cancellationToken);
                if (totalContractPayment == productContract.TotalPrice)
                {
                    totalProductRealIncome += productContract.TotalPrice;
                }
            }

            var productResponce = new ProductIncomeResponse()
            {
                Name = product.Name,
                Description = product.Description,
                ProductCategory = product.ProductCategory.ToString(),
                TotalIncome = totalProductRealIncome
            };

            return productResponce;
        }

        public async Task<TotalIncomeResponse> GetTotalPredictedIncome(TotalIncomeRequest totalIncomeRequest, CancellationToken cancellationToken)
        {
            var products = await _context.Products.ToListAsync(cancellationToken);

            var productIncomeResponceList = new List<ProductIncomeResponse>();

            var totalPredictedIncome = 0m;

            foreach (var product in products)
            {
                var productResponce = await GetProductPredictedIncome(totalIncomeRequest, product.IdProduct, cancellationToken);

                totalPredictedIncome += productResponce.TotalIncome;

                productIncomeResponceList.Add(productResponce);
            }

            return new TotalIncomeResponse()
            {
                Products = productIncomeResponceList,
                TotalIncome = totalPredictedIncome
            };
        }

        public async Task<TotalIncomeResponse> GetTotalRealIncome(TotalIncomeRequest totalIncomeRequest, CancellationToken cancellationToken)
        {
            var products = await _context.Products.ToListAsync(cancellationToken);

            var productIncomeResponceList = new List<ProductIncomeResponse>();

            var totalRealIncome = 0m;

            foreach (var product in products)
            {
                var productResponce = await GetProductRealIncome(totalIncomeRequest, product.IdProduct, cancellationToken);
                
                totalRealIncome += productResponce.TotalIncome;

                productIncomeResponceList.Add(productResponce);
            }

            return new TotalIncomeResponse()
            {
                Products = productIncomeResponceList,
                TotalIncome = totalRealIncome
            };
        }

        private async Task<decimal> GetTotalContractPayment(ProductContract productContract, CancellationToken cancellationToken)
        {
            return await _context.ProductContractPayments
                .Where(e => e.IdProductContract == productContract.IdProductContract)
                .SumAsync(e => e.PaymentValue, cancellationToken);
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
            var totalContractPayment = await GetTotalContractPayment(productContract, cancellationToken);
            
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
