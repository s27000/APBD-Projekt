using System.ComponentModel.DataAnnotations;

namespace Projekt.Models.Domain
{
    public class ProductContract
    {
        public int IdProductContract { get; set; }
        public int IdClient { get; set; }
        public int IdProduct { get; set; }
        public string ProductVersion { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string ProductUpdateDescription { get; set; }
        public int UpdateSupportDuration { get; set; }
        public int? IdDiscount { get; set; }
        public int? Value { get; set; }
        public decimal TotalPrice { get; set; }
        public virtual Client Client { get; set; }
        public virtual Product Product { get; set; }
        public virtual Discount? Discount { get; set; }
        public virtual ICollection<ProductContractPayment> ProductContractPayments { get; set; }
    }
}
