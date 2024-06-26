﻿using System.ComponentModel.DataAnnotations;

namespace Projekt.Models.Client.Request
{
    public class PersonAddRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [Length(9, 9, ErrorMessage = "PhoneNumber must be 9 digits long")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "PhoneNumber must only contain digits.")]
        public string PhoneNumber { get; set; }
        [Required]
        [Length(11, 11, ErrorMessage = "PESEL must be 11 digits long")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "PESEL must only contain digits.")]
        public string PESEL { get; set; }
    }
}
