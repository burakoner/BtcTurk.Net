namespace BtcTurk.Net.Objects
{
    public enum BtcTurkOrderMethod
    {
        Market,
        Limit,
        StopMarket,
        StopLimit
    }
    public enum BtcTurkSymbolStatus
    {
        Trading
    }
    public enum BtcTurkFilterType
    {
        PriceFilter
    }
    public enum BtcTurkPeriod
    {
        OneMinute,
        FiveMinutes,
        ThirtyMinutes,
        OneHour,
        FourHours,
        OneDay,
        OneWeek,
        OneMonth
    }
    public enum BtcTurkOrderSide
    {
        Buy,
        Sell
    }
    public enum BtcTurkOrderStatus
    {
        Untouched,
        Closed,
        Partial,
        Canceled
    }
    public enum BtcTurkTransferType
    {
        Deposit,
        Withdrawal
    }
}
