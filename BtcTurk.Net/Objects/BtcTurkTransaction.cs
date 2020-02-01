using BtcTurk.Net.Converters;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace BtcTurk.Net.Objects
{
    public class BtcTurkTransaction
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
        [JsonProperty("numeratorSymbol")]
        public string NumeratorSymbol { get; set; }
        [JsonProperty("denominatorSymbol")]
        public string DenominatorSymbol { get; set; }
        [JsonProperty("orderType"), JsonConverter(typeof(OrderSideConverter))]
        public BtcTurkOrderSide Side { get; set; }
        [JsonProperty("timestamp"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Time { get; set; }
        [JsonProperty("fee")]
        public decimal Fee { get; set; }
        [JsonProperty("tax")]
        public decimal Tax { get; set; }
    }
}
