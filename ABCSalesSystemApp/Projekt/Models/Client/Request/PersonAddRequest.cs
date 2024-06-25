using System.ComponentModel.DataAnnotations;

namespace Projekt.Models.Client.Request
{
    public class PersonAddRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        [RegularExpression("^[0-9]+$", ErrorMessage = "PhoneNumber must only contain digits.")]
        [Length(9, 9)]
        public string PhoneNumber { get; set; }
        [RegularExpression("^[0-9]+$", ErrorMessage = "PESEL must only contain digits.")]
        [Length(11,11)]
        public string PESEL { get; set; }
    }
}
