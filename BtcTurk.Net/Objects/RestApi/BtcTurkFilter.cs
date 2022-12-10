namespace BtcTurk.Net.Objects.RestApi;

public class BtcTurkFilter
{
    [JsonProperty("filterType"), JsonConverter(typeof(FilterTypeConverter))]
    public BtcTurkSymbolStatus FilterType { get; set; }
    [JsonProperty("minPrice")]
    public decimal MinPrice { get; set; }
    [JsonProperty("maxPrice")]
    public decimal MaxPrice { get; set; }
    [JsonProperty("tickSize")]
    public decimal TickSize { get; set; }
    [JsonProperty("minExchangeValue")]
    public decimal MinExchangeValue { get; set; }
}
