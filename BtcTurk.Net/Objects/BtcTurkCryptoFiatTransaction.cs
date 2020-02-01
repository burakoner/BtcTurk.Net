using BtcTurk.Net.Converters;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace BtcTurk.Net.Objects
{
    public class BtcTurkCryptoFiatTransaction
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("balanceType"), JsonConverter(typeof(TransferTypeConverter))]
        public BtcTurkTransferType TransferType { get; set; }
        [JsonProperty("currencySymbol")]
        public string CurrencySymbol { get; set; }
        [JsonProperty("timestamp"), JsonConverter(typeof(TimestampConverter))]
        public DateTime Time { get; set; }
        [JsonProperty("funds")]
        public decimal Funds { get; set; }
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
        [JsonProperty("fee")]
        public decimal Fee { get; set; }
        [JsonProperty("tax")]
        public decimal Tax { get; set; }
    }
}
