namespace BtcTurk.Net.Objects.RestApi;

public class BtcTurkTrade
{
    [JsonProperty("pair")]
    public string PairSymbol { get; set; } = "";
    [JsonProperty("pairNormalized")]
    public string PairNormalized { get; set; } = "";
    [JsonProperty("numerator")]
    public string Numerator { get; set; } = "";
    [JsonProperty("denominator")]
    public string Denominator { get; set; } = "";

    [JsonProperty("date"), JsonConverter(typeof(TimestampConverter))]
    public DateTime Time { get; set; }

    [JsonProperty("tid")]
    public string TradeId { get; set; } = "";

    [JsonProperty("price")]
    public decimal Price { get; set; }
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    [JsonProperty("side"), JsonConverter(typeof(OrderSideConverter))]
    public BtcTurkOrderSide Side { get; set; }

}
