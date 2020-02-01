using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace BtcTurk.Net.Objects
{
    public class BtcTurkCurrency
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
        [JsonProperty("minWithdrawal")]
        public decimal MinWithdrawal { get; set; }
        [JsonProperty("minDeposit")]
        public decimal MinDeposit { get; set; }
        [JsonProperty("precision")]
        public int Precision { get; set; }
    }
}
