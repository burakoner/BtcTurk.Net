using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace BtcTurk.Net.Objects
{
    public class BtcTurkKlineData
    {
        [JsonProperty("s")]
        public string Status { get; set; }
        [JsonProperty("t")]
        public int[] Times { get; set; }
        [JsonProperty("o")]
        public decimal[] Opens { get; set; }
        [JsonProperty("h")]
        public decimal[] Highs { get; set; }
        [JsonProperty("l")]
        public decimal[] Lows { get; set; }
        [JsonProperty("c")]
        public decimal[] Closes { get; set; }
        [JsonProperty("v")]
        public decimal[] Volumes { get; set; }
    }
}
