using BtcTurk.Net.Converters;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BtcTurk.Net.Objects.SocketObjects
{
    public class BtcTurkStreamTickerAll : BtcTurkStream
    {
        [JsonProperty("items")]
        public List<BtcTurkStreamTickerRow> Items { get; set; } = new List<BtcTurkStreamTickerRow>();
    }
}