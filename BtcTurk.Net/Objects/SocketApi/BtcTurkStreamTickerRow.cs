﻿namespace BtcTurk.Net.Objects.SocketApi;

public class BtcTurkStreamTickerRow
{
    [JsonProperty("B")]
    public decimal Bid { get; set; }
    [JsonProperty("A")]
    public decimal Ask { get; set; }
    [JsonProperty("PS")]
    public string PairSymbol { get; set; } = "";
    [JsonProperty("NS")]
    public string NumeratorSymbol { get; set; } = "";
    [JsonProperty("DS")]
    public string DenominatorSymbol { get; set; } = "";
    [JsonProperty("O")]
    public decimal Open { get; set; }
    [JsonProperty("H")]
    public decimal High { get; set; }
    [JsonProperty("L")]
    public decimal Low { get; set; }
    [JsonProperty("LA")]
    public decimal Close { get; set; }
    [JsonProperty("V")]
    public decimal Volume { get; set; }
    [JsonProperty("AV")]
    public decimal AveragePrice { get; set; }
    [JsonProperty("D")]
    public decimal DailyChange { get; set; }
    [JsonProperty("DP")]
    public decimal DailyChangePercent { get; set; }
    [JsonProperty("PId")]
    public int PairId { get; set; }
    [JsonProperty("Ord")]
    public int OrderNum { get; set; }
}