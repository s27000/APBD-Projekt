using System.ComponentModel.DataAnnotations;

namespace Projekt.Models.Login
{
    public class LoginRequest
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
