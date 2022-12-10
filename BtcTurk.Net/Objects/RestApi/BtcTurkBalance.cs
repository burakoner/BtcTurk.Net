namespace BtcTurk.Net.Objects.RestApi;

public class BtcTurkBalance
{
    [JsonProperty("asset")]
    public string Asset { get; set; } = "";
    [JsonProperty("assetname")]
    public string AssetName { get; set; } = "";
    [JsonProperty("balance")]
    public decimal Balance { get; set; }
    [JsonProperty("locked")]
    public decimal Locked { get; set; }
    [JsonProperty("free")]
    public decimal Free { get; set; }
    [JsonProperty("precision")]
    public int Precision { get; set; }
}
