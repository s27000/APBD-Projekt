using Projekt.Models.Abstract;

namespace Projekt.Models.Domain
{
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}
