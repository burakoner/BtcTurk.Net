using BtcTurk.Net.Converters;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace BtcTurk.Net.Objects.SocketObjects
{
    public class BtcTurkStreamAnswer
    {
        [JsonProperty("type")]
        public int Type { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("ok")]
        public bool OK { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; } = "";
    }
}
