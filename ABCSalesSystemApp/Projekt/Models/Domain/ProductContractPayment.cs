namespace Projekt.Models.Domain
{
    public class ProductContractPayment
    {
        public int IdProductContractPayment { get; set; }
        public int IdProductContract { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public decimal PaymentValue { get; set; }
        public virtual ProductContract ProductContract { get; set; }
    }
}
