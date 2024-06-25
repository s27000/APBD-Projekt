using Projekt.Models.Domain;
using Projekt.Models.Login;

namespace Projekt.Services.Interfaces
{
    public interface ILoginService
    {
        Task Register(RegisterRequest registerRequest, CancellationToken cancellationToken);
        Task Login(LoginRequest loginRequest, CancellationToken cancellationToken);
    }
}
