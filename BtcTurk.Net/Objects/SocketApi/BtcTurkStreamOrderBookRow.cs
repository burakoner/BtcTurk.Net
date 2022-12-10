namespace BtcTurk.Net.Objects.SocketApi;

public class BtcTurkStreamOrderBookRow
{
    [JsonProperty("A")]
    public decimal Amount { get; set; }
    [JsonProperty("P")]
    public decimal Price { get; set; }
}

public class BtcTurkStreamOrderBookDiffRow : BtcTurkStreamOrderBookRow
{
    [JsonProperty("CP")]
    public int CP { get; set; }
}