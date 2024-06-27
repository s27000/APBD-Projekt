namespace Projekt.HttpClients.Interfaces
{
    public interface ICurrencyClient
    {
        Task<decimal> ConvertPLNToCurrency(decimal value, string currency, CancellationToken cancellationToken);
    }
}
