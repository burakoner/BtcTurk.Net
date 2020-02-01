using CryptoExchange.Net.Converters;
using BtcTurk.Net.Objects;
using System.Collections.Generic;
using BtcTurk.Net;

namespace BtcTurk.Net.Converters
{
    public class FilterTypeConverter : BaseConverter<BtcTurkFilterType>
    {
        public FilterTypeConverter() : this(true) { }
        public FilterTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<BtcTurkFilterType, string>> Mapping => new List<KeyValuePair<BtcTurkFilterType, string>>
        {
            new KeyValuePair<BtcTurkFilterType, string>(BtcTurkFilterType.PriceFilter, "PRICE_FILTER"),
        };
    }
}
