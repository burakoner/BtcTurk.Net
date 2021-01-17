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


    /*
    public class BtcTurkAuthenticatedRequest
    {
        [JsonProperty("op")]
        public string Operation { get; set; }
        [JsonProperty("topic")]
        public string Topic { get; set; }
        [JsonProperty("cid")]
        public new string Id { get; set; }

        public BtcTurkAuthenticatedRequest(string id, string operation, string topic)
        {
            Id = id;
            Operation = operation;
            Topic = topic;
        }
    }

    public class BtcTurkSubscribeRequest
    {
        [JsonProperty("sub")]
        public string Topic { get; set; }
        [JsonProperty("id")]
        public new string Id { get; set; }

        public BtcTurkSubscribeRequest(string id, string topic)
        {
            Id = id;
            Topic = topic;
        }
    }
    */


}
