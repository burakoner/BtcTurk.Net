namespace BtcTurk.Net.Objects.RestApi;

public class BtcTurkResources
{
    [JsonProperty("name")]
    public string Name { get; set; } = "";
    [JsonProperty("culture")]
    public string Culture { get; set; } = "";
    [JsonProperty("seoCode")]
    public string SeoCode { get; set; } = "";
    [JsonProperty("flagFileName")]
    public string FlagFileName { get; set; } = "";
    [JsonProperty("rtl")]
    public bool RTL { get; set; }
    [JsonProperty("resources")]
    public Dictionary<string, string> Resources { get; set; } = new Dictionary<string, string>();
}
