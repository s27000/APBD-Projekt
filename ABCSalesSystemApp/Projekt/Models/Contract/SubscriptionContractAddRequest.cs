using System.ComponentModel.DataAnnotations;

namespace Projekt.Models.Contract
{
    public class SubscriptionContractAddRequest
    {
        [Required]
        public int IdSubscription { get; set; }
        [Required]
        public int IdClient { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
    }
}
