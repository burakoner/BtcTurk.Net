namespace BtcTurk.Net.Objects.RestApi;

public class BtcTurkServerExchangeInfo
{
    [JsonProperty("timeZone")]
    public string TimeZone { get; set; }
    [JsonProperty("serverTime"), JsonConverter(typeof(TimestampConverter))]
    public DateTime ServerTime { get; set; }
    [JsonProperty("symbols")]
    public BtcTurkSymbol[] Symbols { get; set; }
    [JsonProperty("currencies")]
    public BtcTurkCurrency[] Currencies { get; set; }
    [JsonProperty("currencyOperationBlocks")]
    public BtcTurkCurrencyOperationBlock[] CurrencyOperationBlocks { get; set; }
}
