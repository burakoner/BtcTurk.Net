using BtcTurk.Net.Converters;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BtcTurk.Net.Objects.SocketObjects
{
    public class BtcTurkStreamOrderBookFull : BtcTurkStream
    {
        [JsonProperty("PS")]
        public string PairSymbol { get; set; } = "";
        [JsonProperty("CS")]
        public int ChangeSet { get; set; }
        [JsonProperty("AO")]
        public List<BtcTurkStreamOrderBookRow> Asks { get; set; } = new List<BtcTurkStreamOrderBookRow>();
        [JsonProperty("BO")]
        public List<BtcTurkStreamOrderBookRow> Bids { get; set; } = new List<BtcTurkStreamOrderBookRow>();
    }
}