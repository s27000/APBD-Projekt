using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Projekt.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        [HttpPost("clients/firm/{krs:int}")]
        public void AddFirm(int krs)
        {

        }

        [HttpPost("clients/person/{pesel:int}")]
        public void AddClient(int pesel)
        {

        }
    }
}
