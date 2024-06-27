using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Projekt.Models.Payment.Request
{
    public class TotalIncomeRequest
    {
        [Required]
        [FromQuery]
        public DateTime DateFrom { get; set; }
        [Required]
        [FromQuery]
        public DateTime DateTo { get; set; }
        public void VerifyBody()
        {
            if (DateTo < DateFrom)
            {
                throw new ArgumentException("DateFrom cannot be greater than DateTo");
            }
        }
    }
}
