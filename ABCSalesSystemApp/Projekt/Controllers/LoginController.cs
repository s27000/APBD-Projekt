using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projekt.Helpers;
using Projekt.Models.Login;
using Projekt.Services.Interfaces;

namespace Projekt.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest, CancellationToken cancellationToken)
        {
            var token = await _loginService.Login(loginRequest, cancellationToken);
            return Ok(new {Token = token});
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest, CancellationToken cancellationToken)
        {
            await _loginService.Register(registerRequest, cancellationToken);
            return Ok("Successfully registered");
        }
    }
}
