namespace TicketHive.Client.Services
{
    public interface ICurrencyService
    {
        decimal? GetExchangeRate(string country);
        Task SetExchangeRates();
        string GetCurrencyName(string country);
    }
}
