using Projekt.Models.Client.Request;
using Projekt.Repositories.Interfaces;
using Projekt.Services.Interfaces;

namespace Projekt.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<int> AddFirmClient(FirmAddRequest firmAddRequest, CancellationToken cancellationToken)
        {
            return await _clientRepository.AddFirmClient(firmAddRequest, cancellationToken);
        }

        public async Task<int> AddPersonClient(PersonAddRequest personAddRequest, CancellationToken cancellationToken)
        {
            return await _clientRepository.AddPersonClient(personAddRequest, cancellationToken);
        }

        public async Task RemoveClient(int idClient, CancellationToken cancellationToken)
        {
            await _clientRepository.RemoveClient(idClient, cancellationToken);
        }

        public async Task UpdateFirmClient(int idClient, FirmModifyRequest firmModifyRequest, CancellationToken cancellationToken)
        {
            await _clientRepository.UpdateFirmClient(idClient, firmModifyRequest, cancellationToken);
        }

        public async Task UpdatePersonClient(int idClient, PersonModifyRequest personModifyRequest, CancellationToken cancellationToken)
        {
            await _clientRepository.UpdatePersonClient(idClient, personModifyRequest, cancellationToken);
        }
    }
}
