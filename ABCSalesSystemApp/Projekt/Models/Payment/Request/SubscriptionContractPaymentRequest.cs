using System.ComponentModel.DataAnnotations;

namespace Projekt.Models.Payment.Request
{
    public class SubscriptionContractPaymentRequest
    {
        [Required]
        public int IdSubscriptionContract { get; set; }
        public string? Description { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public decimal PaymentValue { get; set; }
        public void VerifyBody()
        {
            if (PaymentValue <= 0m)
            {
                throw new ArgumentException("The PaymentValue cannot be smaller or equal than 0");
            }

            if (decimal.Round(PaymentValue, 2) != PaymentValue)
            {
                throw new ArgumentException("The PaymentValue cannot have more than 2 decimal places");
            }
        }
    }
}
