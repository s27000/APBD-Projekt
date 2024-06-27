namespace Projekt.Models.Domain
{
    public class SubscriptionContractPayment
    {
        public int IdSubscriptionContractPayment { get; set; }
        public int IdSubscriptionContract { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public decimal PaymentValue { get; set; }
        public virtual SubscriptionContract SubscriptionContract{ get; set; }
    }
}
