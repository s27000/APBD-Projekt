namespace Projekt.Models.Domain
{
    public class Client
    {
        public int IdClient { get; set; }
        public virtual Firm Firm { get; set; }
        public virtual Person Person { get; set; }
    }
}
