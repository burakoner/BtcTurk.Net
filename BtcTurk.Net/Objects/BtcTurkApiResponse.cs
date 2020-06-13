using CryptoExchange.Net.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BtcTurk.Net.Objects
{
    internal class BtcTurkApiResponse<T>
    {
        [JsonOptionalProperty, JsonProperty("success")]
        internal bool Success { get; set; }
        
        [JsonOptionalProperty, JsonProperty("message")]
        internal string ErrorMessage { get; set; } = "";
        [JsonOptionalProperty, JsonProperty("code")]
        internal int ErrorCode { get; set; }

        [JsonOptionalProperty, JsonProperty("data")]
        public T Data { get; set; }
    }
}
