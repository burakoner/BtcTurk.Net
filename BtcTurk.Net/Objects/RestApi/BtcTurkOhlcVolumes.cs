namespace BtcTurk.Net.Objects.RestApi;

public class BtcTurkOhlcVolumes
{
    [JsonProperty("id")]
    public int Id { get; set; }
    [JsonProperty("daily")]
    public decimal Daily { get; set; }
    [JsonProperty("weekly")]
    public decimal Weekly { get; set; }
    [JsonProperty("monthly")]
    public decimal monthly { get; set; }
    [JsonProperty("numeratorSymbol")]
    public string NumeratorSymbol { get; set; }
}
