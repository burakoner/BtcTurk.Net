using BtcTurk.Net.Converters;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BtcTurk.Net.Objects.SocketObjects
{
    public class BtcTurkStreamTradeList : BtcTurkStream
    {
        [JsonProperty("symbol")]
        public string PairSymbol { get; set; } = "";
        [JsonProperty("items")]
        public List<BtcTurkStreamTradeRow> Items { get; set; } = new List<BtcTurkStreamTradeRow>();
    }
}