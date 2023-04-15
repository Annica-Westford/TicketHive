using Newtonsoft.Json;
using TicketHive.Shared.Api;

namespace TicketHive.Client.Api
{
    public static class ApiHelper
    {
        public static HttpClient HttpClient { get; set; } = new();

        private static Root? exchangeRates;

        public static void InitializeClient()
        {
            HttpClient.BaseAddress = new Uri("https://api.apilayer.com/exchangerates_data/");
        }

        /// <summary>
        /// Retrieves exchange rates using an HTTP GET request to an external API and returns them as a Root object.
        /// </summary>
        /// <returns>
        /// A Task object that represents the asynchronous operation. The task result contains a Root object that represents the exchange rates, or null if the HTTP request fails.
        /// </returns>
        public static async Task<Root?> GetExchangeRates()
        {
            if (exchangeRates == null)
            {
                //string requestUrl = $"convert?from={fromCurrency}&to={toCurrency}&amount={amount}";
                string requestUrl = "latest?symbols=EUR,GBP&base=SEK";

                //create a HttpRequestMessage object specifying the HTTP method and URL
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

                //request.Headers.Add("apikey", apiKey);
                request.Headers.Add("apikey", "Y7SC7DxWRf8EmOddLBwWb1fY67bHWFhy");

                //send the http request and get the response as an HttpResponseMessage
                HttpResponseMessage response = await HttpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    //get the response body as a string
                    string responseString = await response.Content.ReadAsStringAsync();

                    //deserialize the responseString info a C# object
                    exchangeRates = JsonConvert.DeserializeObject<Root>(responseString);
                }
                else
                {
                    return null;
                }

            }

            return exchangeRates;
            
        }
    }
}
