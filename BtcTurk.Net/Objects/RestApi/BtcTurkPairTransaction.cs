namespace BtcTurk.Net.Objects.RestApi;

public class BtcTurkPairTransaction
{
    [JsonProperty("price")]
    public decimal Price { get; set; }
    [JsonProperty("amount")]
    public decimal Amount { get; set; }
    [JsonProperty("orderType")]
    public int OrderType { get; set; }
    [JsonProperty("time"), JsonConverter(typeof(TimestampConverter))]
    public DateTime Time { get; set; }
}
