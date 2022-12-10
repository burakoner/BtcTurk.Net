namespace BtcTurk.Net.Objects.RestApi;

public class BtcTurkCommission
{
    [JsonProperty("paymentProvider")]
    public PaymentProvider[] PaymentProviders { get; set; }
}

public class PaymentProvider
{
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("depositFeePercentage")]
    public decimal DepositFeePercentage { get; set; }
    [JsonProperty("withdrawalFeePercentage")]
    public decimal WithdrawalFeePercentage { get; set; }
    [JsonProperty("maxWithdrawalFeeAmount")]
    public decimal MaxWithdrawalFeeAmount { get; set; }
    [JsonProperty("maxDepositFeeAmount")]
    public decimal MaxDepositFeeAmount { get; set; }
    [JsonProperty("minimumWithdrawalPerTransaction")]
    public decimal MinimumWithdrawalPerTransaction { get; set; }
    [JsonProperty("tax")]
    public decimal Tax { get; set; }
}
