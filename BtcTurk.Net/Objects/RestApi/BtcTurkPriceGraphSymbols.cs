namespace BtcTurk.Net.Objects.RestApi;

public class BtcTurkPriceGraphSymbols
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; } = "";
    [JsonProperty("description")]
    public string Description { get; set; } = "";
    [JsonProperty("minmov")]
    public decimal MinMov { get; set; }
    [JsonProperty("minmov2")]
    public decimal MinMov2 { get; set; }
    [JsonProperty("pricescale")]
    public decimal PriceScale { get; set; }
    [JsonProperty("has_dwm")]
    public bool HasDwm { get; set; }
    [JsonProperty("type")]
    public string Type { get; set; } = "";
    [JsonProperty("ticker")]
    public string Ticker { get; set; } = "";
    [JsonProperty("timezone")]
    public string Timezone { get; set; } = "";
    [JsonProperty("has_seconds")]
    public bool HasSeconds { get; set; }
    [JsonProperty("has_weekly_and_monthly")]
    public bool HasWeeklyAndMonthly { get; set; }
    [JsonProperty("has_daily")]
    public bool HasDaily { get; set; }
    [JsonProperty("has_intraday")]
    public bool HasIntraday { get; set; }
    [JsonProperty(PropertyName = "intraday_multipliers", ItemConverterType = typeof(PeriodEnumConverter))]
    public BtcTurkPeriod[] IntradayMultipliers { get; set; }
    [JsonProperty("name")]
    public string Name { get; set; } = "";
    [JsonProperty("exchange-listed")]
    public string ExchangeListed { get; set; } = "";
    [JsonProperty("exchange-traded")]
    public string ExchangeTraded { get; set; } = "";
    [JsonProperty("has_no_volume")]
    public bool HasNoVolume { get; set; }
    [JsonProperty("session")]
    public string Session { get; set; } = "";
}
