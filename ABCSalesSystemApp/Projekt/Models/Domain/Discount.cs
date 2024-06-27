namespace Projekt.Models.Domain
{
    public class Discount
    {
        public int IdDiscount { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public ICollection<ProductContract> ProductContracts { get; set; }
        public ICollection<SubscriptionContract> SubscriptionContracts { get; set; }
    }
}
