namespace CurrencyExchangeMVC.Models
{
    public  class CurrencyConverterModel
    {
        public decimal Amount { get; set; } = 1;
        public string FromCurrency { get; set; } = "USD";
        public string ToCurrency { get; set; } = "AZN";
        public string Result { get; set; } = "1 USD = 1.7 AZN";

    }
}
