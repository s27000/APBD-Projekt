using Projekt.Models.Abstract;
using System.ComponentModel.DataAnnotations;

namespace Projekt.Models.Login
{
    public class RegisterRequest
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public bool IsAdmin { get; set; }
    }
}
