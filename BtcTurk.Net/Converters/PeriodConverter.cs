using CryptoExchange.Net.Converters;
using BtcTurk.Net.Objects;
using System.Collections.Generic;
using BtcTurk.Net;

namespace BtcTurk.Net.Converters
{
    public class PeriodEnumConverter : BaseConverter<BtcTurkPeriod>
    {
        public PeriodEnumConverter() : this(true) { }
        public PeriodEnumConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<BtcTurkPeriod, string>> Mapping => new List<KeyValuePair<BtcTurkPeriod, string>>
        {
            new KeyValuePair<BtcTurkPeriod, string>(BtcTurkPeriod.OneMinute, "1"),
            new KeyValuePair<BtcTurkPeriod, string>(BtcTurkPeriod.FiveMinutes, "5"),
            new KeyValuePair<BtcTurkPeriod, string>(BtcTurkPeriod.ThirtyMinutes, "30"),
            new KeyValuePair<BtcTurkPeriod, string>(BtcTurkPeriod.OneHour, "60"),
            new KeyValuePair<BtcTurkPeriod, string>(BtcTurkPeriod.FourHours, "240"),
            new KeyValuePair<BtcTurkPeriod, string>(BtcTurkPeriod.OneDay, "1D"),
            new KeyValuePair<BtcTurkPeriod, string>(BtcTurkPeriod.OneWeek, "1W"),
            new KeyValuePair<BtcTurkPeriod, string>(BtcTurkPeriod.OneMonth, "1M"),
        };
    }

    public class PeriodMinutesConverter : BaseConverter<BtcTurkPeriod>
    {
        public PeriodMinutesConverter() : this(true) { }
        public PeriodMinutesConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<BtcTurkPeriod, string>> Mapping => new List<KeyValuePair<BtcTurkPeriod, string>>
        {
            new KeyValuePair<BtcTurkPeriod, string>(BtcTurkPeriod.OneMinute, "1"),
            new KeyValuePair<BtcTurkPeriod, string>(BtcTurkPeriod.FiveMinutes, "5"),
            new KeyValuePair<BtcTurkPeriod, string>(BtcTurkPeriod.ThirtyMinutes, "30"),
            new KeyValuePair<BtcTurkPeriod, string>(BtcTurkPeriod.OneHour, "60"),
            new KeyValuePair<BtcTurkPeriod, string>(BtcTurkPeriod.FourHours, "240"),
            new KeyValuePair<BtcTurkPeriod, string>(BtcTurkPeriod.OneDay, "1440"),
            new KeyValuePair<BtcTurkPeriod, string>(BtcTurkPeriod.OneWeek, "10080"),
            new KeyValuePair<BtcTurkPeriod, string>(BtcTurkPeriod.OneMonth, "43200"),
        };
    }
}
