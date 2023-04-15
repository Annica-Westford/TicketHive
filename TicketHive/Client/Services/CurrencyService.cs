using TicketHive.Client.Api;
using TicketHive.Shared.Api;

namespace TicketHive.Client.Services
{
    public class CurrencyService : ICurrencyService
    {
        private decimal? exchangeRateGBP;
        private decimal? exchangeRateEUR;

        /// <summary>
        /// Sets the exchange rates for Great Britain and the European countries by calling the ApiHelper's GetExchangeRates method.
        /// </summary>
        public async Task SetExchangeRates()
        {
            //Get exchange rates from ApiHelper
            Root? exchangeRates = await ApiHelper.GetExchangeRates();

            if (exchangeRates != null)
            {
                exchangeRateGBP = Convert.ToDecimal(exchangeRates.Rates.GBP);
                exchangeRateEUR = Convert.ToDecimal(exchangeRates.Rates.EUR);
            }
        }

        /// <summary>
        /// Gets the exchange rate for a specified country.
        /// </summary>
        /// <param name="country">The name of the country whose exchange rate is being requested.</param>
        /// <returns>The exchange rate for the specified country, or 1 if the country is null or empty.</returns>
        public decimal? GetExchangeRate(string country)
        {
            if (country == "Sweden" || string.IsNullOrEmpty(country))
            {
                return 1;
            }
            if (country == "Great_Britain")
            {
                return exchangeRateGBP;
            }
            else
            {
                return exchangeRateEUR;
            }
        }

        /// <summary>
        /// Gets the currency name or symbol for a specified country.
        /// </summary>
        /// <param name="country">The name of the country whose currency title is being requested.</param>
        /// <returns>The currency title for the specified country. If null or empty, "kronor" is returned.</returns>
        public string GetCurrencyName(string country)
        {
            if (country == "Sweden" || string.IsNullOrEmpty(country))
            {
                return "kronor";
            }
            else if (country == "Great_Britain")
            {
                return "£";
            }
            else
            {
                return "€";
            }
        }
    }
}
