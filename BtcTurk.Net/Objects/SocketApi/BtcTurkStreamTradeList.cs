namespace BtcTurk.Net.Objects.SocketApi;

public class BtcTurkStreamTradeList : BtcTurkStream
{
    [JsonProperty("symbol")]
    public string PairSymbol { get; set; } = "";
    [JsonProperty("items")]
    public List<BtcTurkStreamTradeRow> Items { get; set; } = new List<BtcTurkStreamTradeRow>();
}