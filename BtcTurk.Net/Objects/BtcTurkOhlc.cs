using BtcTurk.Net.Converters;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace BtcTurk.Net.Objects
{
    public class BtcTurkOhlc
    {
        [JsonProperty("pairSymbol")]
        public string PairSymbol { get; set; } = "";
        [JsonProperty("pairNormalized")]
        public string PairNormalized { get; set; } = "";

        [JsonProperty("time"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Time { get; set; }

        [JsonProperty("open")]
        public decimal Open { get; set; }
        [JsonProperty("high")]
        public decimal High { get; set; }
        [JsonProperty("low")]
        public decimal Low { get; set; }
        [JsonProperty("close")]
        public decimal Close { get; set; }
        
        [JsonProperty("volume")]
        public decimal Volume { get; set; }
        [JsonProperty("average")]
        public decimal Average { get; set; }
        [JsonProperty("dailyChangeAmount")]
        public decimal DailyChangeAmount { get; set; }
        [JsonProperty("dailyChangePercentage")]
        public decimal DailyChangePercentage { get; set; }
    }
}
