using Projekt.Exceptions;
using Projekt.Models.Contract;
using Projekt.Repositories.Interfaces;
using Projekt.Services.Interfaces;

namespace Projekt.Services
{
    public class ContractService : IContractService
    {
        private readonly IContractRepository _contractRepository;
        public ContractService(IContractRepository contractRepository)
        {
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
