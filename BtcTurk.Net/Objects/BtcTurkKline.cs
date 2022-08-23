using BtcTurk.Net.Helpers;
using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace BtcTurk.Net.Objects
{
    public class BtcTurkKline
    {
        private long _openTime;
        public long OpenTime
        {
            get { return _openTime; }
            set { _openTime = value; }
        }

        public DateTime OpenDateTime
        {
            get { return _openTime.FromUnixTimeMilliseconds(); }
            set { _openTime = value.ToUnixTimeMilliseconds(); }
        }

        public decimal Open { get; set; }

        public decimal High { get; set; }

        public decimal Low { get; set; }

        public decimal Close { get; set; }

        public decimal Volume { get; set; }
        public decimal Change
        {
            get { return decimal.Round(this.Close - this.Open, 8); }
        }
        public decimal ChangePercent
        {
            get { return this.Open == 0 ? 0 : decimal.Round(((this.Close - this.Open) / this.Open), 8); }
        }

        /*
        private long _closeTime;
        public long CloseTime
        {
            get { return _closeTime; }
            set { _closeTime = value; }
        }

        public DateTime CloseDateTime
        {
            get { return _closeTime.FromUnixTimeMilliSeconds(); }
            set { _closeTime = value.ToUnixTimeMilliSeconds(); }
        }
        */

    }
}
