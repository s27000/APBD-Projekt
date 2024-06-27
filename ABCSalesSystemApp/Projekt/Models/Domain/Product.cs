using Projekt.Models.Abstract;
using System.Data.SqlTypes;

namespace Projekt.Models.Domain
{
    public class Product
    {
        public int IdProduct { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public decimal AnnualPrice { get; set; }
        public ICollection<ProductContract> ProductContracts { get; set; }
    }
}
