using Moq;
using Projekt.Exceptions;
using Projekt.Models.Contract;
using Projekt.Repositories;
using Projekt.Repositories.Interfaces;
using Projekt.Services;

namespace ProjektTests
{
    [TestClass]
    public class ProjektUnitTests
    {
        [TestMethod]
        public async Task AddProductContract_Should_Throw_Exception_If_DateDifference_Greater_Than_30()
        {
            var contractRepositoryMock = new Mock<IContractRepository>();
            var paymentRepositoryMock = new Mock<IPaymentRepository>();

            var service = new ContractService(contractRepositoryMock.Object, paymentRepositoryMock.Object);

            var cancellationToken = new CancellationToken();

            await Assert.ThrowsExceptionAsync<BuisnessLogicException>(async () =>
            {
                await service.AddProductContract(new ProductContractAddRequest()
                {
                    IdClient = 0,
                    IdProduct = 0,
                    DateFrom = DateTime.Now,
                    DateTo = DateTime.Now.AddDays(31),
                    ProductUpdateDescription = "null",
                    UpdateSupportExtension = 0
                }, cancellationToken);
            });
        }

        [TestMethod]
        public async Task AddProductContract_Should_Throw_Exception_If_DateDifference_Lower_Than_3()
        {
            var contractRepositoryMock = new Mock<IContractRepository>();
            var paymentRepositoryMock = new Mock<IPaymentRepository>();

            var service = new ContractService(contractRepositoryMock.Object, paymentRepositoryMock.Object);

            var cancellationToken = new CancellationToken();

            await Assert.ThrowsExceptionAsync<BuisnessLogicException>(async () =>
            {
                await service.AddProductContract(new ProductContractAddRequest()
                {
                    IdClient = 0,
                    IdProduct = 0,
                    DateFrom = DateTime.Now,
                    DateTo = DateTime.Now.AddDays(2), 
                    ProductUpdateDescription = "null",
                    UpdateSupportExtension = 0
                }, cancellationToken);
            });
        }

        [TestMethod]
        public async Task AddProductContract_Should_Throw_Exception_If_UpdateSupportExtenstion_Is_Greater_that_3()
        {
            var contractRepositoryMock = new Mock<IContractRepository>();
            var paymentRepositoryMock = new Mock<IPaymentRepository>();

            var service = new ContractService(contractRepositoryMock.Object, paymentRepositoryMock.Object);

            var cancellationToken = new CancellationToken();

            await Assert.ThrowsExceptionAsync<BuisnessLogicException>(async () =>
            {
                await service.AddProductContract(new ProductContractAddRequest()
                {
                    IdClient = 0,
                    IdProduct = 0,
                    DateFrom = DateTime.Now,
                    DateTo = DateTime.Now.AddDays(12),
                    ProductUpdateDescription = "null",
                    UpdateSupportExtension = 4
                }, cancellationToken);
            });
        }
    }
}