using System.ComponentModel.DataAnnotations;

namespace Projekt.Models.Domain
{
    public class Person
    {
        public int IdClient { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PESEL { get; set; }
        public virtual Client Client { get; set; }
    }
}
