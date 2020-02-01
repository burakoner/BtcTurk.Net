using CryptoExchange.Net.Converters;
using BtcTurk.Net.Objects;
using System.Collections.Generic;
using BtcTurk.Net;

namespace BtcTurk.Net.Converters
{
    public class OrderSideConverter : BaseConverter<BtcTurkOrderSide>
    {
        public OrderSideConverter() : this(true) { }
        public OrderSideConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<BtcTurkOrderSide, string>> Mapping => new List<KeyValuePair<BtcTurkOrderSide, string>>
        {
            new KeyValuePair<BtcTurkOrderSide, string>(BtcTurkOrderSide.Buy, "buy"),
            new KeyValuePair<BtcTurkOrderSide, string>(BtcTurkOrderSide.Sell, "sell"),
        };
    }
}
