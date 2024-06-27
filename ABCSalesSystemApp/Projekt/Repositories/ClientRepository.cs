using Microsoft.EntityFrameworkCore;
using Projekt.Context;
using Projekt.Exceptions;
using Projekt.Models.Abstract;
using Projekt.Models.Client.Request;
using Projekt.Models.Domain;
using Projekt.Repositories.Interfaces;

namespace Projekt.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext _context;
        public ClientRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<int> AddFirmClient(FirmAddRequest firmAddRequest, CancellationToken cancellationToken)
        {
            var newClient = new Client()
            {
                ClientType = ClientType.Firm,
                Depreciated = false
            };

            await _context.Clients.AddAsync(newClient, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var newFirm = new Firm()
            {
                IdClient = newClient.IdClient,
                Name = firmAddRequest.Name,
                Address = firmAddRequest.Address,
                Email = firmAddRequest.Email,
                PhoneNumber = firmAddRequest.PhoneNumber,
                KRS = firmAddRequest.KRS
            };

            await _context.Firms.AddAsync(newFirm, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return newClient.IdClient;
        }
        public async Task<int> AddPersonClient(PersonAddRequest personAddRequest, CancellationToken cancellationToken)
        {
            var newClient = new Client()
            {
                ClientType = ClientType.Person,
                Depreciated = false
            };

            await _context.Clients.AddAsync(newClient, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var newPerson = new Person()
            {
                IdClient = newClient.IdClient,
                Name = personAddRequest.Name,
                Surname = personAddRequest.Surname,
                Address = personAddRequest.Address,
                Email = personAddRequest.Email,
                PhoneNumber = personAddRequest.PhoneNumber,
                PESEL = personAddRequest.PESEL
            };
            await _context.Persons.AddAsync(newPerson, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return newClient.IdClient;
        }

        public async Task<Client> GetClient(int idClient, CancellationToken cancellationToken)
        {
            var client =  await _context.Clients.Where(e => e.IdClient == idClient).FirstOrDefaultAsync(cancellationToken)
                ?? throw new NotFoundException("Client does not exist");

            if (client.Depreciated)
            {
                throw new NoContentException("Client has been depreciated");
            }
            return client; 
        }

        public async Task RemoveClient(int idClient, CancellationToken cancellationToken)
        {
            var client = await GetClient(idClient, cancellationToken);

            if (client.ClientType == ClientType.Person)
            {
                var person = await _context.Persons.Where(e => e.IdClient == idClient).FirstOrDefaultAsync(cancellationToken);
                person.Name = null;
                person.Surname = null;
                person.Address = null;
                person.Email = null;
                person.PhoneNumber = null;
                person.PESEL = null;
            }

            client.Depreciated = true;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateFirmClient(int idClient, FirmModifyRequest firmModifyRequest, CancellationToken cancellationToken)
        {
            var firm = await _context.Firms.Where(e => e.IdClient == idClient).FirstOrDefaultAsync(cancellationToken)
                ?? throw new NotFoundException("Firm does not exist");

            firm.Name = firmModifyRequest.Name;
            firm.Address = firmModifyRequest.Address;
            firm.Email = firmModifyRequest.Email;
            firm.PhoneNumber = firmModifyRequest.PhoneNumber;

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdatePersonClient(int idClient, PersonModifyRequest personModifyRequest, CancellationToken cancellationToken)
        {
            var person = await _context.Persons.Where(e => e.IdClient == idClient).FirstOrDefaultAsync(cancellationToken)
                ?? throw new NotFoundException("Person does not exist");

            person.Name = personModifyRequest.Name;
            person.Surname = personModifyRequest.Surname;
            person.Address = personModifyRequest.Address;
            person.Email = personModifyRequest.Email;
            person.PhoneNumber = personModifyRequest.PhoneNumber;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
