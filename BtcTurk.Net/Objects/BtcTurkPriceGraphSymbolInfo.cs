using BtcTurk.Net.Converters;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace BtcTurk.Net.Objects
{
    public class BtcTurkPriceGraphSymbolInfo
    {
        [JsonProperty("symbol")]
        public string[] Symbol { get; set; }
        [JsonProperty("description")]
        public string[] Description { get; set; }
        [JsonProperty("exchange_listed")]
        public string ExchangeListed { get; set; } = "";
        [JsonProperty("exchange_traded")]
        public string ExchangeTraded { get; set; } = "";
        [JsonProperty("minmov")]
        public decimal MinMov { get; set; }
        [JsonProperty("minmov2")]
        public decimal MinMov2 { get; set; }
        [JsonProperty("pricescale")]
        public decimal[] PriceScale { get; set; }
        [JsonProperty("has_dwm")]
        public bool HasDwm { get; set; }
        [JsonProperty("type")]
        public string[] Type { get; set; }
        [JsonProperty("ticker")]
        public string[] Ticker { get; set; }
        [JsonProperty("timezone")]
        public string Timezone { get; set; } = "";
        [JsonProperty("session_regular")]
        public string SessionRegular { get; set; } = "";
    }
}
