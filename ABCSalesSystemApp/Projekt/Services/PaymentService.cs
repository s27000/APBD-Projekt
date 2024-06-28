using Microsoft.IdentityModel.Tokens;
using Projekt.HttpClients.Interfaces;
using Projekt.Models.Payment.Request;
using Projekt.Models.Payment.Responce;
using Projekt.Repositories.Interfaces;
using Projekt.Services.Interfaces;

namespace Projekt.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IContractRepository _contractRepository;
        private readonly ICurrencyClient _currencyClient;
        public PaymentService(IPaymentRepository paymentRepository, IContractRepository contractRepository, ICurrencyClient currencyClient)
        {
            _paymentRepository = paymentRepository;
            _currencyClient = currencyClient;
            _contractRepository = contractRepository;
        }
        public async Task<int> AddProductContractPayment(ProductContractPaymentRequest productContractPaymentRequest, CancellationToken cancellationToken)
        {
            productContractPaymentRequest.VerifyBody();
            return await _paymentRepository.AddProductContractPayment(productContractPaymentRequest, cancellationToken);
        }

        public async Task<Tuple<int,int>> AddSubscriptionContractPayment(SubscriptionContractPaymentRequest subscriptionContractPaymentRequest, CancellationToken cancellationToken)
        {
            subscriptionContractPaymentRequest.VerifyBody();
            var paymentId = await _paymentRepository.AddSubscriptionContractPayment(subscriptionContractPaymentRequest, cancellationToken);
            var newContractId = await _contractRepository.GenerateSubscriptionRenewelContract(subscriptionContractPaymentRequest.IdSubscriptionContract, cancellationToken);
            
            return new Tuple<int, int>(paymentId, newContractId);
        }

        public async Task<ProductIncomeResponse> GetProductPredictedIncome(TotalIncomeRequest totalIncomeRequest, int idProduct, string? currency, CancellationToken cancellationToken)
        {
            totalIncomeRequest.VerifyBody();
            var productPredictedIncome = await _paymentRepository.GetProductPredictedIncome(totalIncomeRequest, idProduct, cancellationToken);

            if (!currency.IsNullOrEmpty() && currency != "PLN")
            {
                productPredictedIncome.ContractIncome = await _currencyClient.ConvertPLNToCurrency(productPredictedIncome.ContractIncome, currency, cancellationToken);
                productPredictedIncome.SubscriptionIncome = await _currencyClient.ConvertPLNToCurrency(productPredictedIncome.SubscriptionIncome, currency, cancellationToken);
                productPredictedIncome.TotalProductIncome = await _currencyClient.ConvertPLNToCurrency(productPredictedIncome.TotalProductIncome, currency, cancellationToken);
            }

            return productPredictedIncome;
        }

        public async Task<ProductIncomeResponse> GetProductRealIncome(TotalIncomeRequest totalIncomeRequest, int idProduct, string? currency, CancellationToken cancellationToken)
        {
            totalIncomeRequest.VerifyBody();
            var productRealIncome = await _paymentRepository.GetProductRealIncome(totalIncomeRequest, idProduct, cancellationToken);

            if(!currency.IsNullOrEmpty() && currency != "PLN")
            {
                productRealIncome.ContractIncome = await _currencyClient.ConvertPLNToCurrency(productRealIncome.ContractIncome, currency, cancellationToken);
                productRealIncome.SubscriptionIncome = await _currencyClient.ConvertPLNToCurrency(productRealIncome.SubscriptionIncome, currency, cancellationToken);
                productRealIncome.TotalProductIncome = await _currencyClient.ConvertPLNToCurrency(productRealIncome.TotalProductIncome, currency, cancellationToken);
            }

            return productRealIncome;
        }

        public async Task<TotalIncomeResponse> GetTotalPredictedIncome(TotalIncomeRequest totalIncomeRequest, string? currency, CancellationToken cancellationToken)
        {
            totalIncomeRequest.VerifyBody();
            var totalPredictedIncome = await _paymentRepository.GetTotalPredictedIncome(totalIncomeRequest, cancellationToken);

            if (!currency.IsNullOrEmpty() && currency != "PLN")
            {
                totalPredictedIncome.Currency = currency;
                foreach (var product in totalPredictedIncome.Products)
                {
                    product.ContractIncome = await _currencyClient.ConvertPLNToCurrency(product.ContractIncome, currency, cancellationToken);
                    product.SubscriptionIncome = await _currencyClient.ConvertPLNToCurrency(product.SubscriptionIncome, currency, cancellationToken);
                    product.TotalProductIncome = await _currencyClient.ConvertPLNToCurrency(product.TotalProductIncome, currency, cancellationToken);
                }
                totalPredictedIncome.TotalContractIncome = await _currencyClient.ConvertPLNToCurrency(totalPredictedIncome.TotalContractIncome, currency, cancellationToken);
                totalPredictedIncome.TotalSubscriptionIncome = await _currencyClient.ConvertPLNToCurrency(totalPredictedIncome.TotalSubscriptionIncome, currency, cancellationToken);
                totalPredictedIncome.TotalIncome = await _currencyClient.ConvertPLNToCurrency(totalPredictedIncome.TotalIncome, currency, cancellationToken);
            }
            else
            {
                totalPredictedIncome.Currency = "PLN";
            }

            return totalPredictedIncome;
        }

        public async Task<TotalIncomeResponse> GetTotalRealIncome(TotalIncomeRequest totalIncomeRequest, string? currency, CancellationToken cancellationToken)
        {
            totalIncomeRequest.VerifyBody();
            var totalRealIncome =  await _paymentRepository.GetTotalRealIncome(totalIncomeRequest, cancellationToken);

            if (!currency.IsNullOrEmpty() && currency != "PLN")
            {
                totalRealIncome.Currency = currency;
                foreach (var product in totalRealIncome.Products)
                {
                    product.ContractIncome = await _currencyClient.ConvertPLNToCurrency(product.ContractIncome, currency, cancellationToken);
                    product.SubscriptionIncome = await _currencyClient.ConvertPLNToCurrency(product.SubscriptionIncome, currency, cancellationToken);
                    product.TotalProductIncome = await _currencyClient.ConvertPLNToCurrency(product.TotalProductIncome, currency, cancellationToken);
                }
                totalRealIncome.TotalContractIncome = await _currencyClient.ConvertPLNToCurrency(totalRealIncome.TotalContractIncome, currency, cancellationToken);
                totalRealIncome.TotalSubscriptionIncome = await _currencyClient.ConvertPLNToCurrency(totalRealIncome.TotalSubscriptionIncome, currency, cancellationToken);
                totalRealIncome.TotalIncome = await _currencyClient.ConvertPLNToCurrency(totalRealIncome.TotalIncome, currency, cancellationToken);
            }
            else
            {
                totalRealIncome.Currency = "PLN";
            }

            return totalRealIncome;
        }
    }
}
