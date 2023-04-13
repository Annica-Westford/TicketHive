using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketHive.Shared.Api
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Info
    {
        [JsonProperty("rate")]
        public double? Rate { get; set; }

        [JsonProperty("timestamp")]
        public int? Timestamp { get; set; }
    }

    public class Query
    {
        [JsonProperty("amount")]
        public int? Amount { get; set; }

        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }
    }

    public class Root
    {
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("historical")]
        public string Historical { get; set; }

        [JsonProperty("info")]
        public Info Info { get; set; }

        [JsonProperty("query")]
        public Query Query { get; set; }

        [JsonProperty("result")]
        public double? Result { get; set; }

        [JsonProperty("success")]
        public bool? Success { get; set; }
    }


}
