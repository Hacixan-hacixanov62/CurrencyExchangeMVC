namespace CurrencyExchangeMVC.Services.Interfaces
{
    public interface ICurrencyService
    {
        Task<decimal> GetExchangeRate(string fromCurrency, string toCurrency);
    }
}
