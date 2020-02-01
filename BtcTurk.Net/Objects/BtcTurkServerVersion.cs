using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace BtcTurk.Net.Objects
{
    public class BtcTurkServerVersion
    {
        [JsonProperty("version")]
        public string Version { get; set; }
    }
}
