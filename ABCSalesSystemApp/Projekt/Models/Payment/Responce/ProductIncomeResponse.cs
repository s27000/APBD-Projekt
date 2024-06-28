namespace Projekt.Models.Payment.Responce
{
    public class ProductIncomeResponse
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProductCategory { get; set; }
        public decimal ContractIncome { get; set; }
        public decimal SubscriptionIncome { get; set; }
        public decimal TotalProductIncome { get; set; }
    }
}
