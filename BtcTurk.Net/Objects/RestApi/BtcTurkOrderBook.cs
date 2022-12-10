namespace BtcTurk.Net.Objects.RestApi;

public class BtcTurkOrderBook
{
    [JsonProperty("timestamp"), JsonConverter(typeof(TimestampConverter))]
    public DateTime Timestamp { get; set; }

    [JsonProperty("asks")]
    public BtcTurkOrderBookEntry[] Asks { get; set; }
    [JsonProperty("bids")]
    public BtcTurkOrderBookEntry[] Bids { get; set; }
}

[JsonConverter(typeof(ArrayConverter))]
public class BtcTurkOrderBookEntry : ISymbolOrderBookEntry
{
    /// <summary>
    /// The price for this entry
    /// </summary>
    [ArrayProperty(0)]
    public decimal Price { get; set; }
    /// <summary>
    /// The quantity for this entry
    /// </summary>
    [ArrayProperty(1)]
    public decimal Quantity { get; set; }

}
