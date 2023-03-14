namespace BtcTurk.Net.Objects.RestApi;

public class BtcTurkFilter
{
    [JsonProperty("filterType"), JsonConverter(typeof(FilterTypeConverter))]
    public BtcTurkFilterType FilterType { get; set; }
    [JsonProperty("minPrice")]
    public decimal MinPrice { get; set; }
    [JsonProperty("maxPrice")]
    public decimal MaxPrice { get; set; }
    [JsonProperty("tickSize")]
    public decimal TickSize { get; set; }
    [JsonProperty("minExchangeValue")]
    public decimal MinExchangeValue { get; set; }
    [JsonProperty("minAmount")]
    public decimal? MinAmount { get; set; }
    [JsonProperty("maxAmount")]
    public decimal? MaxAmount { get; set; }
}
