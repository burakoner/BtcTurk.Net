using BtcTurk.Net.Converters;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace BtcTurk.Net.Objects
{
    public class BtcTurkOpenOrders
    {
        [JsonProperty("asks")]
        public BtcTurkOrder[] Asks { get; set; }
        [JsonProperty("bids")]
        public BtcTurkOrder[] Bids { get; set; }
    }
}
