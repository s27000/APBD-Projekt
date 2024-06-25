using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Projekt.Exceptions;
using System.Security.Cryptography;

namespace Projekt.Helpers
{
    public static class SecurityHandler
    {
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public static void VerifyPassword(string password, string hashedPassword)
        {
            if(!BCrypt.Net.BCrypt.Verify(password, hashedPassword))
            {
                throw new UnverifiedException("The Password is incorrect");
            }
        }
    }
}
