using BtcTurk.Net.Objects.RestApi;

namespace BtcTurk.Net.Objects.SocketApi;

public class BtcTurkStreamKline : BtcTurkStream
{
    [JsonProperty("D"), JsonConverter(typeof(EpochConverter))]
    public DateTime Date { get; set; }
    [JsonProperty("P")]
    public string Pair { get; set; }
    [JsonProperty(PropertyName = "R", ItemConverterType = typeof(PeriodMinutesConverter))]
    public BtcTurkPeriod Period { get; set; }
    [JsonProperty("O")]
    public decimal Open { get; set; }
    [JsonProperty("H")]
    public decimal High { get; set; }
    [JsonProperty("L")]
    public decimal Low { get; set; }
    [JsonProperty("C")]
    public decimal Close { get; set; }
    [JsonProperty("V")]
    public decimal Volume { get; set; }
}
