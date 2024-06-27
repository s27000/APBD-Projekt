using Microsoft.EntityFrameworkCore;
using Projekt.Context;
using Projekt.Exceptions;
using Projekt.Models.Domain;
using Projekt.Models.Payment;
using Projekt.Repositories.Interfaces;

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

        private void PaymentDateIsValid(DateTime paymentDate, DateTime contractDateFrom, DateTime contractDateTo)
        {
            if(paymentDate > contractDateTo || paymentDate < contractDateFrom)
            {
                throw new ArgumentException("The PaymentDate is either overdue, or too early");
            }
        }

        private async Task ProductContractPaymentIsNotFulfilledOrOverflown(ProductContract productContract, decimal paymentValue, CancellationToken cancellationToken)
        {
            var totalContractPayment = await _context.ProductContractPayments
                .Where(e => e.IdProductContract == productContract.IdProductContract)
                .SumAsync(e => e.PaymentValue, cancellationToken);

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
