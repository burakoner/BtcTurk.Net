using BtcTurk.Net.Objects.RestApi;

namespace BtcTurk.Net.Converters;

public class SymbolMethodConverter : BaseConverter<BtcTurkOrderMethod>
{
    public SymbolMethodConverter() : this(true) { }
    public SymbolMethodConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<BtcTurkOrderMethod, string>> Mapping => new List<KeyValuePair<BtcTurkOrderMethod, string>>
    {
        new KeyValuePair<BtcTurkOrderMethod, string>(BtcTurkOrderMethod.Market, "MARKET"),
        new KeyValuePair<BtcTurkOrderMethod, string>(BtcTurkOrderMethod.Limit, "LIMIT"),
        new KeyValuePair<BtcTurkOrderMethod, string>(BtcTurkOrderMethod.StopMarket, "STOP_MARKET"),
        new KeyValuePair<BtcTurkOrderMethod, string>(BtcTurkOrderMethod.StopLimit, "STOP_LIMIT"),
    };
}
