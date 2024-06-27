namespace Projekt.Exceptions
{
    public class AlreadyPurchasedException : Exception
    {
        public AlreadyPurchasedException(string message) : base(message) { }
    }
}
