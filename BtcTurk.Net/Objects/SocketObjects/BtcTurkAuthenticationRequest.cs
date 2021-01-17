using Newtonsoft.Json;

namespace BtcTurk.Net.Objects.SocketObjects
{
    public class BtcTurkAuthenticationRequest
    {
        [JsonProperty("op")]
        public string Operation { get; set; }
        public string AccessKeyId { get; set; }
        public string SignatureMethod { get; set; }
        public string SignatureVersion { get; set; }
        public string Timestamp { get; set; }
        public string Signature { get; set; }
    }
}
