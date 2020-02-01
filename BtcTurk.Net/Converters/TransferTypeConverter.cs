using CryptoExchange.Net.Converters;
using BtcTurk.Net.Objects;
using System.Collections.Generic;
using BtcTurk.Net;

namespace BtcTurk.Net.Converters
{
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
}
