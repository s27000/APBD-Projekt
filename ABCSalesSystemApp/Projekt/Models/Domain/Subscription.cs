namespace Projekt.Models.Domain
{
    public class Subscription
    {
        public int IdSubscription { get; set; }
        public int IdProduct { get; set; }
        public string Name { get; set; }
        public int SubscriptionDurationInMonths { get; set; }
        public decimal MonthlyPrice { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<SubscriptionContract> SubscriptionContracts { get; set; }
    }
}
