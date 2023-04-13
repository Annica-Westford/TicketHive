using TicketHive.Client.Api;
using TicketHive.Shared.Api;

namespace TicketHive.Client.Services
{
    public class CurrencyService
    {
        public async Task<decimal?> ConvertAmount(string country, decimal price)
        {
            //Get exchange rates from ApiHelper
            Root? root = await new ApiHelper().GetExchangeRates();

            if (root != null)
            {
                if (country == "Sweden" || string.IsNullOrEmpty(country))
                {
                    return price;
                }
                else if (country == "Great_Britain")
                {
                    return price * Convert.ToDecimal(root.Rates.GBP);
                }
                else
                {
                    return price * Convert.ToDecimal(root.Rates.EUR);
                }
            }

            return null;
        }
    }
}
