namespace BtcTurk.Net.Objects.Core;

public class BtcTurkClientOptions : ClientOptions
{
    public static BtcTurkClientOptions Default { get; set; } = new BtcTurkClientOptions();


    private BtcTurkSingleClientOptions _singleApiOptions = new(BtcTurkApiAddresses.Default.ApiAddress)
    {
        RateLimiters = new List<IRateLimiter>
        {
            new RateLimiter()
            .AddTotalRateLimit(600, TimeSpan.FromSeconds(600))
            .AddPartialEndpointLimit("/api/v1/users/balances", 120, TimeSpan.FromSeconds(600), HttpMethod.Get, true, false)

            .AddPartialEndpointLimit("/api/v1/order", 300, TimeSpan.FromSeconds(600), HttpMethod.Post, true, false)
            .AddPartialEndpointLimit("/api/v1/order", 300, TimeSpan.FromSeconds(600), HttpMethod.Delete, true, false)
            .AddPartialEndpointLimit("/api/v1/openOrders", 330, TimeSpan.FromSeconds(600), HttpMethod.Get, true, false)
            .AddPartialEndpointLimit("/api/v1/allOrders", 330, TimeSpan.FromSeconds(600), HttpMethod.Get, true, false)

            .AddPartialEndpointLimit("/api/v1/trades", 90, TimeSpan.FromSeconds(600), HttpMethod.Get, true, false)

            .AddPartialEndpointLimit("/api/v1/users/transactions/trade", 90, TimeSpan.FromSeconds(600), HttpMethod.Get, true, false)
            .AddPartialEndpointLimit("/api/v1/users/transactions/trade/*", 90, TimeSpan.FromSeconds(600), HttpMethod.Get, true, false)
            .AddPartialEndpointLimit("/api/v1/users/transactions/crypto", 90, TimeSpan.FromSeconds(600), HttpMethod.Get, true, false)
            .AddPartialEndpointLimit("/api/v1/users/transactions/fiat", 90, TimeSpan.FromSeconds(600), HttpMethod.Get, true, false)

            .AddPartialEndpointLimit("/api/v2/ticker", 600, TimeSpan.FromSeconds(600), HttpMethod.Get, true, false)
            .AddPartialEndpointLimit("/api/v2/orderBook", 60, TimeSpan.FromSeconds(600), HttpMethod.Get, true, false)
        }
    };

    public new ApiCredentials ApiCredentials
    {
        get => base.ApiCredentials;
        set => base.ApiCredentials = value;
    }

    public BtcTurkSingleClientOptions SingleApiOptions
    {
        get => _singleApiOptions;
        set => _singleApiOptions = new BtcTurkSingleClientOptions(_singleApiOptions, value);
    }

    public BtcTurkClientOptions() : this(Default)
    {
    }

    internal BtcTurkClientOptions(BtcTurkClientOptions baseOn) : base(baseOn)
    {
        if (baseOn == null)
            return;

        ApiCredentials = baseOn.ApiCredentials?.Copy();
        _singleApiOptions = new BtcTurkSingleClientOptions(baseOn.SingleApiOptions, null);
    }
}

public class BtcTurkSingleClientOptions : RestApiClientOptions
{
    public new ApiCredentials ApiCredentials
    {
        get => base.ApiCredentials;
        set => base.ApiCredentials = value;
    }

    public BtcTurkSingleClientOptions()
    {
    }

    internal BtcTurkSingleClientOptions(string baseAddress) : base(baseAddress)
    {
    }

    internal BtcTurkSingleClientOptions(BtcTurkSingleClientOptions baseOn, BtcTurkSingleClientOptions newValues) : base(baseOn, newValues)
    {
        ApiCredentials = newValues?.ApiCredentials?.Copy() ?? baseOn.ApiCredentials?.Copy();
    }
}
