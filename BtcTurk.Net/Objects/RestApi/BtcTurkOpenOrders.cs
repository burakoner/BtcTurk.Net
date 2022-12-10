namespace BtcTurk.Net.Objects.RestApi;

public class BtcTurkOpenOrders
{
    [JsonProperty("asks")]
    public BtcTurkOrder[] Asks { get; set; }
    [JsonProperty("bids")]
    public BtcTurkOrder[] Bids { get; set; }
}
