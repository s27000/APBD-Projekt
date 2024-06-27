using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Projekt.Models.Contract
{
    public class ProductContractAddRequest
    {
        [Required]
        public int IdClient { get; set; }
        [Required]
        public int IdProduct { get; set; }
        [Required]
        public DateTime DateFrom { get; set; }
        [Required]
        public DateTime DateTo { get; set; }
        [Required]
        public string ProductUpdateDescription { get; set; }
        [Required]
        public int UpdateSupportExtension { get; set; }
        public void VerifyBody()
        {
            if (DateTo < DateFrom)
            {
                throw new ArgumentException("DateFrom cannot be greater than DateTo");
            }
        }
    }
}
