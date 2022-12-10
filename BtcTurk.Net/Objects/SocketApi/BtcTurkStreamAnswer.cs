namespace BtcTurk.Net.Objects.SocketApi;

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
