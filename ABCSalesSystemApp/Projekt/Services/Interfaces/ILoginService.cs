using Projekt.Models.Domain;
using Projekt.Models.Login;

namespace Projekt.Services.Interfaces
{
    public interface ILoginService
    {
        Task Register(RegisterRequest registerRequest, CancellationToken cancellationToken);
        Task<string> Login(LoginRequest loginRequest, CancellationToken cancellationToken);
    }
}
