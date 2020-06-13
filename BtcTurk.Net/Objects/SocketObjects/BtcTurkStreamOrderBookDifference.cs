using BtcTurk.Net.Converters;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BtcTurk.Net.Objects.SocketObjects
{
    public class BtcTurkStreamOrderBookDifference : BtcTurkStream
    {
        [JsonProperty("PS")]
        public string PairSymbol { get; set; } = "";
        [JsonProperty("CS")]
        public int ChangeSet { get; set; }
        [JsonProperty("AO")]
        public List<BtcTurkStreamOrderBookDiffRow> Asks { get; set; } = new List<BtcTurkStreamOrderBookDiffRow>();
        [JsonProperty("BO")]
        public List<BtcTurkStreamOrderBookDiffRow> Bids { get; set; } = new List<BtcTurkStreamOrderBookDiffRow>();
    }
}