using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projekt.Context;
using Projekt.Models.Abstract;
using Projekt.Models.Client.Request;
using Projekt.Services.Interfaces;

namespace Projekt.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(AppDbContext appDbContext, IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost("firm")]
        public async Task<IActionResult> AddFirmClient(FirmAddRequest firmAddRequest, CancellationToken cancellationToken)
        {
            var newFirmClientId = await _clientService.AddFirmClient(firmAddRequest, cancellationToken);
            return Ok("Successfully added new firm client, with the id of: " + newFirmClientId);
        }

        [HttpPost("person")]
        public async Task<IActionResult> AddPersonClient(PersonAddRequest personAddRequest, CancellationToken cancellationToken)
        {
            var newPersonClientId = await _clientService.AddPersonClient(personAddRequest, cancellationToken);
            return Ok("Successfully added new person client, with the id of: " + newPersonClientId);
        }

        [HttpDelete("/{idClient:int}")]
        public async Task<IActionResult> RemoveClient(int idClient, CancellationToken cancellationToken)
        {
            await _clientService.RemoveClient(idClient, cancellationToken);
            return Ok("The client of the id: " + idClient + ", is now depreciated");
        }
        [HttpPut("firm/{idClient:int}/update")]
        public async Task<IActionResult> UpdateFirmClient(int idClient, FirmModifyRequest firmModifyRequest, CancellationToken cancellationToken)
        {
            await _clientService.UpdateFirmClient(idClient, firmModifyRequest, cancellationToken);
            return Ok("The firm of the client id: " + idClient + ", has been updated");
        }
        [HttpPut("person/{idClient:int}/update")]
        public async Task<IActionResult> UpdatePersonClient(int idClient, PersonModifyRequest personModifyRequest, CancellationToken cancellationToken)
        {
            await _clientService.UpdatePersonClient(idClient, personModifyRequest, cancellationToken);
            return Ok("The person of the client id: " + idClient + ", has been updated");
        }
    }
}
