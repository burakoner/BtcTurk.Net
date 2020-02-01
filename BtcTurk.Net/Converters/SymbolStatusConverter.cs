using CryptoExchange.Net.Converters;
using BtcTurk.Net.Objects;
using System.Collections.Generic;
using BtcTurk.Net;

namespace BtcTurk.Net.Converters
{
    public class SymbolStatusConverter : BaseConverter<BtcTurkSymbolStatus>
    {
        public SymbolStatusConverter() : this(true) { }
        public SymbolStatusConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<BtcTurkSymbolStatus, string>> Mapping => new List<KeyValuePair<BtcTurkSymbolStatus, string>>
        {
            new KeyValuePair<BtcTurkSymbolStatus, string>(BtcTurkSymbolStatus.Trading, "TRADING"),
        };
    }
}
