namespace BtcTurk.Net.Objects.RestApi;

public class BtcTurkCurrencyOperationBlock
{
    [JsonProperty("currencySymbol")]
    public string Symbol { get; set; }

    [JsonProperty("depositDisabled")]
    public bool DepositDisabled { get; set; }

    [JsonProperty("withdrawalDisabled")]
    public bool WithdrawalDisabled { get; set; }
}
