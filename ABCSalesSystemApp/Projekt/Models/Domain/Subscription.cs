namespace Projekt.Models.Domain
{
    public class Subscription
    {
        public int IdSubscription { get; set; }
        public int IdProduct { get; set; }
        public string Name { get; set; }
        public int SubscriptionRenewelInMonths { get; set; }
        public decimal Price { get; set; }
        public virtual Product Product { get; set; }
    }
}
