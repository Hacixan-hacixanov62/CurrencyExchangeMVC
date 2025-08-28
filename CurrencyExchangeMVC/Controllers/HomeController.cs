using System.Diagnostics;
using CurrencyExchangeMVC.Models;
using CurrencyExchangeMVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchangeMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICurrencyService _currencyService;

        public HomeController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        public IActionResult Index()
        {
            var model = new CurrencyConverterModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Convert(CurrencyConverterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var exchangeRate = await _currencyService.GetExchangeRate(model.FromCurrency, model.ToCurrency);
                    var convertedAmount = model.Amount * exchangeRate;
                    model.Result = $"{model.Amount} {model.FromCurrency} = {convertedAmount:0.##} {model.ToCurrency}";
                }
                catch (Exception ex)
                {
                    model.Result = $"Error: {ex.Message}";
                }
            }

            return View("Index", model);
        }

    }
}
