using CryptoExchange.Net.Objects;
using System;

namespace BtcTurk.Net.Objects.ClientObjects
{
    public class BtcTurkSocketClientOptions : BaseSocketClientOptions
    {
        public bool DemoTradingService { get; set; } = false;

        public static BtcTurkSocketClientOptions Default { get; set; } = new BtcTurkSocketClientOptions()
        {
            SocketSubscriptionsCombineTarget = 100,
            MaxSocketConnections = 50
        };

        public new CryptoExchange.Net.Authentication.ApiCredentials ApiCredentials
        {
            get => base.ApiCredentials;
            set => base.ApiCredentials = value;
        }

        private BtcTurkSocketSingleClientOptions _singleStreamsOptions = new BtcTurkSocketSingleClientOptions();
        public BtcTurkSocketSingleClientOptions SingleStreamsOptions
        {
            get => _singleStreamsOptions;
            set => _singleStreamsOptions = new BtcTurkSocketSingleClientOptions(_singleStreamsOptions, value);
        }

        public BtcTurkSocketClientOptions() : this(Default)
        {
        }

        internal BtcTurkSocketClientOptions(BtcTurkSocketClientOptions baseOn) : base(baseOn)
        {
            if (baseOn == null)
                return;

            ApiCredentials = baseOn.ApiCredentials?.Copy();
            _singleStreamsOptions = new BtcTurkSocketSingleClientOptions(baseOn.SingleStreamsOptions, null);
        }
    }

    public class BtcTurkSocketSingleClientOptions : ApiClientOptions
    {
        public new CryptoExchange.Net.Authentication.ApiCredentials ApiCredentials
        {
            get => base.ApiCredentials;
            set => base.ApiCredentials = value;
        }

        public BtcTurkSocketSingleClientOptions()
        {
        }

        internal BtcTurkSocketSingleClientOptions(BtcTurkSocketSingleClientOptions baseOn, BtcTurkSocketSingleClientOptions newValues) : base(baseOn, newValues)
        {
            ApiCredentials = newValues?.ApiCredentials?.Copy() ?? baseOn.ApiCredentials?.Copy();
        }
    }

}
