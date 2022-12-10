namespace BtcTurk.Net.Objects.RestApi;

public class BtcTurkPriceGraphConfig
{
    [JsonProperty("supports_search")]
    public bool SupportsSearch { get; set; }
    [JsonProperty("supports_group_request")]
    public bool SupportsGroupRequest { get; set; }
    [JsonProperty("supports_marks")]
    public bool SupportsMarks { get; set; }
    [JsonProperty("supports_time")]
    public bool SupportsTime { get; set; }
    [JsonProperty(PropertyName = "supported_resolutions", ItemConverterType = typeof(PeriodEnumConverter))]
    public BtcTurkPeriod[] SupportedResolutions { get; set; }
}
