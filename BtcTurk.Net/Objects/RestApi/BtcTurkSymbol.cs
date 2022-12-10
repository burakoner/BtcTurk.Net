namespace BtcTurk.Net.Objects.RestApi;

public class BtcTurkSymbol
{
    [JsonProperty("id")]
    public int Id { get; set; }
    [JsonProperty("name")]
    public string Name { get; set; } = "";
    [JsonProperty("nameNormalized")]
    public string nameNormalized { get; set; } = "";
    [JsonProperty("status"), JsonConverter(typeof(SymbolStatusConverter))]
    public BtcTurkSymbolStatus Status { get; set; }
    [JsonProperty("numerator")]
    public string Numerator { get; set; } = "";
    [JsonProperty("denominator")]
    public string Denominator { get; set; } = "";
    [JsonProperty("numeratorScale")]
    public int NumeratorScale { get; set; }
    [JsonProperty("denominatorScale")]
    public int DenominatorScale { get; set; }
    [JsonProperty("hasFraction")]
    public bool HasFraction { get; set; }
    [JsonProperty("filters")]
    public BtcTurkFilter[] Filters { get; set; }
    [JsonProperty(PropertyName = "orderMethods", ItemConverterType = typeof(SymbolMethodConverter))]
    public BtcTurkOrderMethod[] OrderMethods { get; set; }
}
