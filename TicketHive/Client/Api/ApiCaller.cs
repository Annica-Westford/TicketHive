﻿namespace TicketHive.Client.Api
{
    public class ApiCaller
    {
        //Har kört InitializeClient i Program.cs
        //Har lagt till ApiAccessKey i appsettings.json - ska kunna nås via Iconfiguration
        //Har lagt till en ApiModel - OBS tänk på att Result kommer tillbaka som DOUBLE
        //Enligt Daniel: Det viktiga är att api key sätts in i headern på ett request, inte i query eller url

        private readonly IConfiguration config;
        private readonly string apiKey;

        public static HttpClient HttpClient { get; set; } = new();

        public ApiCaller(IConfiguration config) 
        {
            this.config = config;
            apiKey = config.GetValue<string>("ApiAccessKey");
        }

        public static void InitializeClient()
        {
            HttpClient.BaseAddress = new Uri("https://api.apilayer.com/exchangerates_data/");
        }

        //public async Task<decimal?> ConvertCurrencyAsync(string fromCurrency, string toCurrency, decimal amount)
        //{
        //    string requestUrl = $"convert?from={fromCurrency}&to={toCurrency}&amount={amount}";

        //    //create a HttpRequestMessage object specifying the HTTP method and URL
        //    var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

        //    request.Headers.Add("apikey", apiKey);


        //}
        
    }
}