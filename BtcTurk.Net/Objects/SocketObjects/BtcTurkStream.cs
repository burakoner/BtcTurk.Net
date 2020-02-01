using BtcTurk.Net.Converters;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace BtcTurk.Net.Objects.SocketObjects
{
    public class BtcTurkStream
    {
        [JsonProperty("channel")]
        public string Channel { get; set; }
        [JsonProperty("event")]
        public string Event { get; set; }
        [JsonProperty("type")]
        public int Type { get; set; }
    }
}
