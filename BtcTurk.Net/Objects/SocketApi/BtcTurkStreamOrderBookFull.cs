namespace BtcTurk.Net.Objects.SocketApi;

public class BtcTurkStreamOrderBookFull : BtcTurkStream
{
    [JsonProperty("PS")]
    public string PairSymbol { get; set; } = "";
    [JsonProperty("CS")]
    public int ChangeSet { get; set; }
    [JsonProperty("AO")]
    public List<BtcTurkStreamOrderBookRow> Asks { get; set; } = new List<BtcTurkStreamOrderBookRow>();
    [JsonProperty("BO")]
    public List<BtcTurkStreamOrderBookRow> Bids { get; set; } = new List<BtcTurkStreamOrderBookRow>();
}