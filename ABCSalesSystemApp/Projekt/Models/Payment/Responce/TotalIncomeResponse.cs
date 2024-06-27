namespace Projekt.Models.Payment.Responce
{
    public class TotalIncomeResponse
    {
        public string? Currency { get; set; }
        public List<ProductIncomeResponse> Products { get; set; }
        public decimal TotalIncome { get; set; }
    }
}
