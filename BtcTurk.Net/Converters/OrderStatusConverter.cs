using BtcTurk.Net.Objects.RestApi;

namespace BtcTurk.Net.Converters;

public class OrderStatusConverter : BaseConverter<BtcTurkOrderStatus>
{
    public OrderStatusConverter() : this(true) { }
    public OrderStatusConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<BtcTurkOrderStatus, string>> Mapping => new List<KeyValuePair<BtcTurkOrderStatus, string>>
    {
        new KeyValuePair<BtcTurkOrderStatus, string>(BtcTurkOrderStatus.Untouched, "Untouched"),
        new KeyValuePair<BtcTurkOrderStatus, string>(BtcTurkOrderStatus.Closed, "Closed"),
        new KeyValuePair<BtcTurkOrderStatus, string>(BtcTurkOrderStatus.Partial, "Partial"),
        new KeyValuePair<BtcTurkOrderStatus, string>(BtcTurkOrderStatus.Canceled, "Canceled"),
    };
}