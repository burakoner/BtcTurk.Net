using CryptoExchange.Net.Objects;
using System;
using BtcTurk.Net.Interfaces;

namespace BtcTurk.Net
{
    public class BtcTurkClientOptions: RestClientOptions
    {
        public BtcTurkClientOptions():base("https://api.btcturk.com/api")
        {
            // BaseAddress = "https://api-dev.btcturk.com/api";
            // BaseAddress = "https://api.btcturk.com/api";
        }

        public BtcTurkClientOptions Copy()
        {
            return Copy<BtcTurkClientOptions>();
        }
    }
}
