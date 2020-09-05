using BtcTurk.Net.Converters;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BtcTurk.Net.Objects.SocketObjects
{
    public class BtcTurkStreamOrderBookRow
    {
        [JsonProperty("A")]
        public decimal Amount { get; set; }
        [JsonProperty("P")]
        public decimal Price { get; set; }
    }

    public class BtcTurkStreamOrderBookDiffRow : BtcTurkStreamOrderBookRow
    {
        [JsonProperty("CP")]
        public int CP { get; set; }
    }
}