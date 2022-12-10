namespace BtcTurk.Net.Objects.SocketApi;

public class BtcTurkStream
{
    [JsonProperty("channel")]
    public string Channel { get; set; } = "";
    [JsonProperty("event")]
    public string Event { get; set; } = "";
    [JsonProperty("type")]
    public int Type { get; set; }
}
