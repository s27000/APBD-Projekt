namespace Projekt.Models.Domain
{
    public class SubscriptionContract
    {
        public int IdSubscriptionContract { get; set; }
        public int IdClient { get; set; }
        public int IdSubscription { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int? IdDiscount { get; set; }
        public int? DiscountValue { get; set; }
        public decimal Price { get; set; }
        public virtual Client Client { get; set; }
        public virtual Subscription Subscription { get; set; }
        public virtual Discount? Discount { get; set; }
        public virtual ICollection<SubscriptionContractPayment> SubscriptionContractPayments { get; set; }
    }
}
