using TicketHive.Client.Api;
using TicketHive.Shared.Api;

namespace TicketHive.Client.Services
{
    public class CurrencyService : ICurrencyService
    {
        public async Task<decimal?> ConvertAmount(string country, decimal price)
        {
            //Get exchange rates from ApiHelper
            Root? exchangeRates = await ApiHelper.GetExchangeRates();

            if (exchangeRates != null)
            {
                if (country == "Sweden" || string.IsNullOrEmpty(country))
                {
                    return price;
                }
                else if (country == "Great_Britain")
                {
                    return price * Convert.ToDecimal(exchangeRates.Rates.GBP);
                }
                else
                {
                    return price * Convert.ToDecimal(exchangeRates.Rates.EUR);
                }
            }

            return null;
        }

        public string GetCurrencyTitle(string country)
        {
            if (country == "Sweden" || string.IsNullOrEmpty(country))
            {
                return "kronor";
            }
            else if (country == "Great_Britain")
            {
                return "GBP";
            }
            else
            {
                return "€";
            }
        }
    }
}
