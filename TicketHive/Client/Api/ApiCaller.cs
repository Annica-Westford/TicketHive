namespace TicketHive.Client.Api
{
    public class ApiCaller
    {
        //Har kört InitializeClient i Program.cs
        //Har lagt till ApiAccessKey i appsettings.json - ska kunna nås via Iconfiguration
        //Enligt Daniel: Det viktiga är att api key sätts in i headern på ett request, inte i query eller url

        private readonly IConfiguration config;

        public static HttpClient HttpClient { get; set; } = new();

        public ApiCaller(IConfiguration config) 
        {
            this.config = config;
        }

        public static void InitializeClient()
        {
            HttpClient.BaseAddress = new Uri("https://api.apilayer.com/exchangerates_data/");
        }
    }
}
