using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projekt.Context;
using Projekt.Models.Client.Request;

namespace Projekt.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public ClientController(IConfiguration configuration, AppDbContext appDbContext)
        {
            _configuration = configuration;
            _context = appDbContext;
        }

        [Authorize]
        [HttpPost("clients/firm/{krs:int}")]
        public void AddFirm(int krs)
        {

        }

        [Authorize]
        [HttpPost("clients/person/{pesel:int}")]
        public void AddPerson(int pesel, PersonAddRequest personAddRequest)
        {

        }
    }
}
