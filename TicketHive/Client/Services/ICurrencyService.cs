namespace TicketHive.Client.Services
{
    public interface ICurrencyService
    {
        Task<decimal?> GetExchangeRate(string country);
        Task SetExchangeRates();
        //Task<decimal?> ConvertAmount(string country, decimal price);
        string GetCurrencyTitle(string country);
    }
}
