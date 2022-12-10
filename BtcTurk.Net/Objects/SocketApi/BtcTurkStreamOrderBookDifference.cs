namespace BtcTurk.Net.Objects.SocketApi;

public class BtcTurkStreamOrderBookDifference : BtcTurkStream
{
    [JsonProperty("PS")]
    public string PairSymbol { get; set; } = "";
    [JsonProperty("CS")]
    public int ChangeSet { get; set; }
    [JsonProperty("AO")]
    public List<BtcTurkStreamOrderBookDiffRow> Asks { get; set; } = new List<BtcTurkStreamOrderBookDiffRow>();
    [JsonProperty("BO")]
    public List<BtcTurkStreamOrderBookDiffRow> Bids { get; set; } = new List<BtcTurkStreamOrderBookDiffRow>();
}