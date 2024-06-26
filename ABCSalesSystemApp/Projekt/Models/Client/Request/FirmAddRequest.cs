using System.ComponentModel.DataAnnotations;

namespace Projekt.Models.Client.Request
{
    public class FirmAddRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [Length(9, 9, ErrorMessage = "PhoneNumber must be 9 digits long")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "PhoneNumber must only contain digits.")]
        public string PhoneNumber { get; set; }
        [Required]
        [Length(9, 14, ErrorMessage = "KRS must be between 9 and 14 digits long")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "KRS must only contain digits.")]
        public string KRS { get; set; }
    }
}
