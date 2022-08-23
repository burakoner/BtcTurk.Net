using CryptoExchange.Net.Sockets;
using Newtonsoft.Json;

namespace BtcTurk.Net.Objects.SocketObjects
{
    public class BtcTurkSocketRequest
    {
        [JsonProperty("type")]
        public int Type { get; set; }
        [JsonProperty("channel")]
        public string Channel { get; set; }
        [JsonProperty("event")]
        public string Event { get; set; }
        [JsonProperty("join")]
        public bool Join { get; set; }

        public BtcTurkSocketRequest(int type, string channel, string evt, bool join)
        {
            Type = type;
            Channel = channel;
            Event = evt;
            Join = join;
        }

        public object[] RequestObject()
        {
            return new object[] { this.Type, this };
        }

        public string RequestString()
        {
            return JsonConvert.SerializeObject(this.RequestObject());
        }
    }

    public class BtcTurkSocketLoginRequest
    {
        [JsonProperty("type")]
        public int Type { get; set; }
        
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        public BtcTurkSocketLoginRequest(int type, int id, string token, string username)
        {
            Id = id;
            Type = type;
            Token = token;
            Username = username;
        }

        public object[] RequestObject()
        {
            return new object[] { this.Type, this };
        }

        public string RequestString()
        {
            return JsonConvert.SerializeObject(this.RequestObject());
        }
    }
    
    public class BtcTurkSocketLoginResponse
    {
        [JsonProperty("type")]
        public int Type { get; set; }
        
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("ok")]
        public bool OK { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }

}
