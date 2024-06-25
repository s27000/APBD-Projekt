using System.ComponentModel.DataAnnotations;

namespace Projekt.Models.Domain
{
    public class Firm
    {
        public int IdClient { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string KRS { get; set; }
        public virtual Client Client { get; set; }
    }
}
