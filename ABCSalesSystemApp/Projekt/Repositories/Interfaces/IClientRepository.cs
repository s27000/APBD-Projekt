using Projekt.Models.Client.Request;

namespace Projekt.Repositories.Interfaces
{
    public interface IClientRepository
    {
        Task<int> AddPersonClient(PersonAddRequest personAddRequest, CancellationToken cancellationToken);
        Task<int> AddFirmClient(FirmAddRequest firmAddRequest, CancellationToken cancellationToken);
        Task RemoveClient(int idClient, CancellationToken cancellationToken);
        Task UpdatePersonClient(int idClient, PersonModifyRequest personModifyRequest, CancellationToken cancellationToken);
        Task UpdateFirmClient(int idClient, FirmModifyRequest firmModifyRequest, CancellationToken cancellationToken);
    }
}
