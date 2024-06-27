using Projekt.HttpClients.Interfaces;
using Projekt.Models.Currency;
using System.Text.Json;
using System.Threading;

namespace Projekt.HttpClients
{
    public class CurrencyClient : ICurrencyClient
    {
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "https://api.nbp.pl/api/exchangerates/tables/a/?format=json";
        public CurrencyClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<decimal> ConvertPLNToCurrency(decimal value, string currency, CancellationToken cancellationToken)
        {
            var exchangeRatesList = await GenerateExchangeRatesList(cancellationToken);

            var exchangeRate = exchangeRatesList
                .Where(e => e.Code == currency)
                .Select(e => e.Rate)
                .FirstOrDefault();

            if(exchangeRate == 0)
            {
                throw new ArgumentException("Exchange rate has not been found");
            } 

            return Math.Round(value / exchangeRate,2);
        }

        private async Task<List<ExchangeRate>> GenerateExchangeRatesList(CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync(ApiUrl, cancellationToken);
            response.EnsureSuccessStatusCode();

            var exchangeRatesList = new List<ExchangeRate>();

            var jsonString = await response.Content.ReadAsStringAsync(cancellationToken);

            var jsonElement = JsonDocument.Parse(jsonString).RootElement;

            //var ratesArray = jsonElement.EnumerateArray()
            foreach (var table in jsonElement.EnumerateArray())
            {
                var ratesArray = table.GetProperty("rates");
                foreach (var rate in ratesArray.EnumerateArray())
                {
                    var exchangeRate = new ExchangeRate()
                    {
                        Code = rate.GetProperty("code").GetString(),
                        Rate = rate.GetProperty("mid").GetDecimal()
                    };
                    exchangeRatesList.Add(exchangeRate);
                }
            }

            return exchangeRatesList;
        }
    }
}
