using Projekt.Models.Domain;
using Projekt.Models.Login;

namespace Projekt.Repositories.Interfaces
{
    public interface ILoginRepository
    {
        Task AddUser(RegisterRequest registerRequest, CancellationToken cancellationToken);
        Task<User> GetUser(LoginRequest loginRequest, CancellationToken cancellationToken);
    }
}
