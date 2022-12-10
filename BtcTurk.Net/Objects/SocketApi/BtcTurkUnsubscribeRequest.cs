namespace BtcTurk.Net.Objects.SocketApi;

public class BtcTurkUnsubscribeRequest
{
    [JsonProperty("unsub")]
    public string Topic { get; set; }

    [JsonProperty("id")]
    public string Id { get; set; }

    public BtcTurkUnsubscribeRequest(string id, string topic)
    {
        Topic = topic;
        Id = id;
    }
}

public class BtcTurkAuthUnsubscribeRequest
{
    [JsonProperty("op")]
    public string Operation { get; set; }
    [JsonProperty("topic")]
    public string Topic { get; set; }

    [JsonProperty("cid")]
    public string Id { get; set; }

    public BtcTurkAuthUnsubscribeRequest(string id, string topic)
    {
        Operation = "unsub";
        Topic = topic;
        Id = id;
    }
}
