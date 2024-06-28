using Projekt.Exceptions;
using Projekt.Models.Contract;
using Projekt.Models.Payment.Request;
using Projekt.Repositories;
using Projekt.Repositories.Interfaces;
using Projekt.Services.Interfaces;

namespace Projekt.Services
{
    public class ContractService : IContractService
    {
        private readonly IContractRepository _contractRepository;
        private readonly IPaymentRepository _paymentRepository;
        public ContractService(IContractRepository contractRepository, IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
            _contractRepository = contractRepository;
        }
        public async Task<int> AddProductContract(ProductContractAddRequest productContractAddRequest, CancellationToken cancellationToken)
        {
            productContractAddRequest.VerifyBody();
            TimespanIsSufficient(productContractAddRequest.DateFrom, productContractAddRequest.DateTo);
            UpdateSupportRangeIsSufficient(productContractAddRequest.UpdateSupportExtension);
            
            var updateSupportDuration = 1 + productContractAddRequest.UpdateSupportExtension;

            return await _contractRepository.AddProductContract(productContractAddRequest, updateSupportDuration, cancellationToken);
        }

        public async Task<Tuple<int, int, int>> AddSubscriptonContract(SubscriptionContractAddRequest subscriptionContractAddRequest, CancellationToken cancellationToken)
        {
            var subscriptionContractId = await _contractRepository.AddSubscriptionContract(subscriptionContractAddRequest, cancellationToken);
            Console.WriteLine(subscriptionContractId);
            var subscriptionContract = await _contractRepository.GetSubscriptionContract(subscriptionContractId, cancellationToken);
            Console.WriteLine(subscriptionContract.IdSubscriptionContract);

            Console.WriteLine("SubscriptionContract DateFrom: " + subscriptionContract.DateFrom);

            var contractPaymentId = await _paymentRepository.AddSubscriptionContractPayment(new SubscriptionContractPaymentRequest()
            {
                IdSubscriptionContract = subscriptionContractId,
                Description = "Subscription Purchase",
                Date = subscriptionContract.DateFrom,
                PaymentValue = subscriptionContract.Price
            }, cancellationToken);

            var newContractId = await _contractRepository.GenerateSubscriptionRenewelContract(subscriptionContract.IdSubscriptionContract, cancellationToken);

            return new Tuple<int, int, int>(subscriptionContractId, contractPaymentId, newContractId);
        }

        public void TimespanIsSufficient(DateTime dateFrom, DateTime dateTo)
        {
            int dayDifference = (dateTo - dateFrom).Days;
            if(dayDifference < 3 || dayDifference > 30)
            {
                throw new BuisnessLogicException("The time difference between dates must be between 3 and 30 days");
            }
        } 

        public void UpdateSupportRangeIsSufficient(int updateSupportExtension)
        {
            if(updateSupportExtension < 0 || updateSupportExtension > 3)
            {
                throw new BuisnessLogicException("The UpdateSupportExtension must be between 0 to 3 years");
            }
        }
    }
}
