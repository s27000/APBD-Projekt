using Projekt.Helpers;
using Projekt.Models.Domain;
using Projekt.Models.Login;
using Projekt.Repositories.Interfaces;
using Projekt.Services.Interfaces;

namespace Projekt.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public async Task Login(LoginRequest loginRequest, CancellationToken cancellationToken)
        {
            var hashedUser = await _loginRepository.GetUser(loginRequest, cancellationToken);
            SecurityHandler.VerifyPassword(loginRequest.Password, hashedUser.Password);
        }

        public async Task Register(RegisterRequest registerRequest, CancellationToken cancellationToken)
        {
            await _loginRepository.AddUser(registerRequest, cancellationToken);
        }
    }
}
