namespace BtcTurk.Net.Objects.RestApi;

public class BtcTurkServerPing
{
    [JsonProperty("pong")]
    public bool Pong { get; set; }
}
