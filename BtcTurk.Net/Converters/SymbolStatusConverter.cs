using BtcTurk.Net.Objects.RestApi;

namespace BtcTurk.Net.Converters;

public class SymbolStatusConverter : BaseConverter<BtcTurkSymbolStatus>
{
    public SymbolStatusConverter() : this(true) { }
    public SymbolStatusConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<BtcTurkSymbolStatus, string>> Mapping => new List<KeyValuePair<BtcTurkSymbolStatus, string>>
    {
        new KeyValuePair<BtcTurkSymbolStatus, string>(BtcTurkSymbolStatus.Trading, "TRADING"),
    };
}
