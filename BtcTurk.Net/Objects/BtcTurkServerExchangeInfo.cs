using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace BtcTurk.Net.Objects
{
    public class BtcTurkServerExchangeInfo
    {
        [JsonProperty("timeZone")]
        public string TimeZone { get; set; }
        [JsonProperty("serverTime"), JsonConverter(typeof(TimestampConverter))]
        public DateTime ServerTime { get; set; }
        [JsonProperty("symbols")]
        public BtcTurkSymbol[] Symbols { get; set; }
        [JsonProperty("currencies")]
        public  BtcTurkCurrency[] Currencies { get; set; }
    }
}
