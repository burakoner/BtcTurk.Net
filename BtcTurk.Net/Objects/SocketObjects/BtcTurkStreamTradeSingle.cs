using BtcTurk.Net.Converters;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BtcTurk.Net.Objects.SocketObjects
{
    public class BtcTurkStreamTradeSingle: BtcTurkStream
    {
        [JsonProperty("D"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Time { get; set; }
        [JsonProperty("I")]
        public string TradeId { get; set; } = "";
        [JsonProperty("A")]
        public decimal Amount { get; set; }
        [JsonProperty("P")]
        public decimal Price { get; set; }
        [JsonProperty("PS")]
        public string PairSymbol { get; set; } = "";
        [JsonProperty("S")]
        public int S { get; set; }
    }
}