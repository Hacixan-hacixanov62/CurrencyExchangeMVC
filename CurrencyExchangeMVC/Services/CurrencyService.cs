using CurrencyExchangeMVC.Services.Interfaces;
using System.Text.Json;

namespace CurrencyExchangeMVC.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly HttpClient _httpClient;
        private const string BaseURl = "https://cdn.jsdelivr.net/npm/@fawazahmed0/currency-api@latest/v1/currencies";

        public CurrencyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<decimal> GetExchangeRate(string fromCurrency, string toCurrency)
        {
            try
            {
                var url = $"{BaseURl}/{fromCurrency.ToLower()}.json";
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"API request failed with status code: {response.StatusCode}");
                }

                var content = await response.Content.ReadAsStringAsync();
                var jsonDocument = JsonDocument.Parse(content);

                // Get the root element
                var root = jsonDocument.RootElement;

                // The rates are in a property named after the fromCurrency (lowercase)
                if (root.TryGetProperty(fromCurrency.ToLower(), out var ratesObject))
                {
                    if (ratesObject.TryGetProperty(toCurrency.ToLower(), out var rateValue))
                    {
                        return rateValue.GetDecimal();
                    }
                    throw new Exception($"Target currency {toCurrency} not found in rates");
                }
                throw new Exception($"Source currency {fromCurrency} not found in response");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching exchange rate: {ex.Message}");
            }
        }
    }
}
