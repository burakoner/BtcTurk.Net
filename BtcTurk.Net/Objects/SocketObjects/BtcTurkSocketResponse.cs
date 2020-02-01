using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace BtcTurk.Net.Objects.SocketObjects
{
    /*
    internal abstract class BtcTurkResponse
    {
        internal abstract bool Success { get; }
        /*
        [JsonProperty("err-code")]
        public string ErrorCode { get; set; }
        [JsonProperty("err-msg")]
        public string ErrorMessage { get; set; }
        * /
    }
    */

    [JsonConverter(typeof(WSObjectConverter))]
    public class BtcTurkSocketResponse // : BtcTurkResponse
    {
        // internal override bool Success => true; // OK == true;

        public int Type { get; set; }
        public string Data { get; set; }

        /*
        [JsonProperty("type")]
        public int Type { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("ok")]
        public bool OK { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        */
    }

    internal class WSObjectConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var foo = value as BtcTurkSocketResponse;
            var obj = new object[] { foo.Type, foo.Data };
            serializer.Serialize(writer, obj);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var arr = ReadArrayObject(reader, serializer);
            return new BtcTurkSocketResponse
            {
                Type = (int)arr[0],
                Data = arr[1].ToString(),
            };
        }

        private JArray ReadArrayObject(JsonReader reader, JsonSerializer serializer)
        {
            var arr = serializer.Deserialize<JToken>(reader) as JArray;
            if (arr == null || arr.Count != 2)
                throw new JsonSerializationException("Expected array of length 2");
            return arr;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(BtcTurkSocketResponse);
        }
    }

    public class BtcTurkSocketBaseResponse
    {
        [JsonProperty("type")]
        public int Type { get; set; }
        [JsonProperty("channel")]
        public string Channel { get; set; }
        [JsonProperty("event")]
        public string Event { get; set; }
        [JsonProperty("join")]
        public bool Join { get; set; }
        public BtcTurkSocketBaseResponse()
        {
        }

        public BtcTurkSocketBaseResponse(int type, string channel, string evt, bool join)
        {
            Type = type;
            Channel = channel;
            Event = evt;
            Join = join;
        }

        public object[] RequestObject()
        {
            return new object[] { this.Type, JsonConvert.SerializeObject(this) };
        }

        public string RequestString()
        {
            return JsonConvert.SerializeObject(this.RequestObject());
        }
    }

    public class BtcTurkWelcomeResponse
    {
        [JsonProperty("type")]
        public int Type { get; set; }
        [JsonProperty("current")]
        public string Current { get; set; }
        [JsonProperty("min")]
        public string Min { get; set; }
    }
}
