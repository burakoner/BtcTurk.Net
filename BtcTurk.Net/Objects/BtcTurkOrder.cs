using BtcTurk.Net.Converters;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace BtcTurk.Net.Objects
{
    public class BtcTurkOrder
    {
        [JsonProperty("id")]
        public long OrderId { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
        [JsonProperty("quantity")]
        public decimal Quantity { get; set; }
        [JsonProperty("pairSymbol")]
        public string PairSymbol { get; set; }
        [JsonProperty("pairSymbolNormalized")]
        public string PairSymbolNormalized { get; set; }
        [JsonProperty("type"), JsonConverter(typeof(OrderSideConverter))]
        public BtcTurkOrderSide Side { get; set; }
        [JsonProperty("method"), JsonConverter(typeof(OrderMethodConverter))]
        public BtcTurkOrderMethod Method { get; set; }
        [JsonProperty("orderClientId")]
        public string ClientOrderId { get; set; }
        [JsonProperty("time"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Datetime { get; set; }
        [JsonProperty("updateTime"), JsonConverter(typeof(TimestampConverter))]
        public DateTime UpdateTime { get; set; }
        [JsonProperty("status"), JsonConverter(typeof(OrderStatusConverter))]
        public BtcTurkOrderStatus Status { get; set; }
    }
}
