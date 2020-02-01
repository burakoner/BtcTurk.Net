using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace BtcTurk.Net.Objects
{
    public class BtcTurkServerPing
    {
        [JsonProperty("pong")]
        public bool Pong { get; set; }
    }
}
