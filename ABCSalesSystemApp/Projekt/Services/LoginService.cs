using Projekt.Helpers;
using Projekt.Models.Domain;
using Projekt.Models.Login;
using Projekt.Repositories.Interfaces;
using Projekt.Services.Interfaces;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Projekt.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IConfiguration _configuration;
        public LoginService(ILoginRepository loginRepository, IConfiguration configuration)
        {
            _loginRepository = loginRepository;
            _configuration = configuration;
        }

        public string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> Login(LoginRequest loginRequest, CancellationToken cancellationToken)
        {
            var hashedUser = await _loginRepository.GetUser(loginRequest, cancellationToken);
            SecurityHandler.VerifyPassword(loginRequest.Password, hashedUser.Password);
            return GenerateJwtToken(hashedUser);
        }

        public async Task Register(RegisterRequest registerRequest, CancellationToken cancellationToken)
        {
            await _loginRepository.AddUser(registerRequest, cancellationToken);
        }
    }
}
