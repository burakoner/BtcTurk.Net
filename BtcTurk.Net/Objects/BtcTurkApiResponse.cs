using CryptoExchange.Net.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BtcTurk.Net.Objects
{
    public class BtcTurkApiResponse<T>
    {
        [JsonOptionalProperty, JsonProperty("success")]
        public bool Success { get; set; }
        
        [JsonOptionalProperty, JsonProperty("message")]
        public string ErrorMessage { get; set; } = "";
        [JsonOptionalProperty, JsonProperty("code")]
        public int ErrorCode { get; set; }

        [JsonOptionalProperty, JsonProperty("data")]
        public T Data { get; set; }
    }
}
