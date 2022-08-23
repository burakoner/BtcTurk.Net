using CryptoExchange.Net.Objects;
using Newtonsoft.Json;

namespace BtcTurk.Net.Objects
{
    public class BtcTurkApiResponse<T>
    {
        [JsonProperty("success", NullValueHandling = NullValueHandling.Ignore)]
        public bool Success { get; set; }

        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string ErrorMessage { get; set; }
        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public int ErrorCode { get; set; }

        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public T Data { get; set; }
    }

    public class BtcTurkApiError : Error
    {
        public BtcTurkApiError(int? code, string message, object data) : base(code, message, data)
        {
        }
    }
}
