using Microsoft.EntityFrameworkCore;
using Projekt.Context;
using Projekt.Exceptions;
using Projekt.Helpers;
using Projekt.Models.Abstract;
using Projekt.Models.Domain;
using Projekt.Models.Login;
using Projekt.Repositories.Interfaces;

namespace Projekt.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly AppDbContext _context;
        public LoginRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUser(LoginRequest loginRequest, CancellationToken cancellationToken)
        {
            var user = await _context.Users.Where(user => user.Login == loginRequest.Login)
                .FirstOrDefaultAsync(cancellationToken);

            if (user == null)
            {
                throw new NotFoundException("User does not exist");
            }

            return user;
        }

        public async Task AddUser(RegisterRequest registerRequest, CancellationToken cancellationToken)
        {
            var hashedPassword = SecurityHandler.HashPassword(registerRequest.Password);
            var newUser = new User()
            {
                Login = registerRequest.Login,
                Password = hashedPassword,
                Role = registerRequest.IsAdmin? Role.Admin : Role.Worker
            };

            await _context.Users.AddAsync(newUser, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
