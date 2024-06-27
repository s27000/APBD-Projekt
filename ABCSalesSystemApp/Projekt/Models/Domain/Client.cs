using Projekt.Models.Abstract;

namespace Projekt.Models.Domain
{
    public class Client
    {
        public int IdClient { get; set; }
        public ClientType ClientType { get; set; }
        public bool Depreciated { get; set; }
        public virtual Firm Firm { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<ProductContract> ProductContracts { get; set; }
    }
}
