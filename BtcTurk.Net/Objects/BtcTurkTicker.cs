using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace BtcTurk.Net.Objects
{
    public class BtcTurkTicker
    {
        [JsonProperty("pair")]
        public string Pair { get; set; } = "";
        [JsonProperty("pairNormalized")]
        public string PairNormalized { get; set; } = "";

        [JsonProperty("timestamp"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Timestamp { get; set; }

        [JsonProperty("ask")]
        public decimal Ask { get; set; }
        [JsonProperty("bid")]
        public decimal Bid { get; set; }

        [JsonProperty("open")]
        public decimal Open { get; set; }
        [JsonProperty("high")]
        public decimal High { get; set; }
        [JsonProperty("low")]
        public decimal Low { get; set; }
        [JsonProperty("last")]
        public decimal Close { get; set; }
        
        [JsonProperty("volume")]
        public decimal Volume { get; set; }
        [JsonProperty("average")]
        public decimal Average { get; set; }
        [JsonProperty("daily")]
        public decimal Daily { get; set; }
        [JsonProperty("dailyPercent")]
        public decimal DailyPercent { get; set; }
        [JsonProperty("denominatorSymbol")]
        public string DenominatorSymbol { get; set; } = "";
        [JsonProperty("numeratorSymbol")]
        public string NumeratorSymbol { get; set; } = "";
        [JsonProperty("order")]
        public int Order { get; set; }
    }
}
