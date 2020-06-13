using BtcTurk.Net.Converters;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace BtcTurk.Net.Objects
{
    public class BtcTurkPlacedOrder
    {
        [JsonProperty("id")]
        public long OrderId { get; set; }
        [JsonProperty("quantity")]
        public decimal Quantity { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }
        [JsonProperty("stopPrice")]
        public decimal? StopPrice { get; set; }
        [JsonProperty("newOrderClientId")]
        public string ClientOrderId { get; set; } = "";
        [JsonProperty("type"), JsonConverter(typeof(OrderSideConverter))]
        public BtcTurkOrderSide Side { get; set; }
        [JsonProperty("method"), JsonConverter(typeof(OrderMethodConverter))]
        public BtcTurkOrderMethod Method { get; set; }
        [JsonProperty("pairSymbol")]
        public string PairSymbol { get; set; } = "";
        [JsonProperty("pairSymbolNormalized")]
        public string PairSymbolNormalized { get; set; } = "";
        [JsonProperty("datetime"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Datetime { get; set; }
    }
}
