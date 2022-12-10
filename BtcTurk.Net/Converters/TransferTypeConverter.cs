using BtcTurk.Net.Objects.RestApi;

namespace BtcTurk.Net.Converters;

public class TransferTypeConverter : BaseConverter<BtcTurkTransferType>
{
    public TransferTypeConverter() : this(true) { }
    public TransferTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<BtcTurkTransferType, string>> Mapping => new List<KeyValuePair<BtcTurkTransferType, string>>
    {
        new KeyValuePair<BtcTurkTransferType, string>(BtcTurkTransferType.Deposit, "Deposit"),
        new KeyValuePair<BtcTurkTransferType, string>(BtcTurkTransferType.Withdrawal, "Withdrawal"),
    };
}
