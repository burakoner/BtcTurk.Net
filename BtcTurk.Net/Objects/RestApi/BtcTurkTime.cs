namespace BtcTurk.Net.Objects.RestApi;

public class BtcTurkTime
{
    [JsonProperty("serverTime"), JsonConverter(typeof(TimestampConverter))]
    public DateTime ServerTime { get; set; }
}
