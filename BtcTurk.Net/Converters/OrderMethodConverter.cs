using CryptoExchange.Net.Converters;
using BtcTurk.Net.Objects;
using System.Collections.Generic;
using BtcTurk.Net;

namespace BtcTurk.Net.Converters
{
    public class OrderMethodConverter : BaseConverter<BtcTurkOrderMethod>
    {
        public OrderMethodConverter() : this(true) { }
        public OrderMethodConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<BtcTurkOrderMethod, string>> Mapping => new List<KeyValuePair<BtcTurkOrderMethod, string>>
        {
            new KeyValuePair<BtcTurkOrderMethod, string>(BtcTurkOrderMethod.Market, "market"),
            new KeyValuePair<BtcTurkOrderMethod, string>(BtcTurkOrderMethod.Limit, "limit"),
            new KeyValuePair<BtcTurkOrderMethod, string>(BtcTurkOrderMethod.StopLimit, "stoplimit"),
        };
    }
}
