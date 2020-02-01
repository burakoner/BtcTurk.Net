using CryptoExchange.Net.Objects;
using System;
using BtcTurk.Net.Interfaces;

namespace BtcTurk.Net
{
    public class BtcTurkSocketClientOptions : SocketClientOptions
    {
        public BtcTurkSocketClientOptions() : base("wss://ws-feed-pro.btcturk.com")
        {
            // BaseAddress = "wss://ws-feed-sandbox.btctrader.com";
            // BaseAddress = "wss://ws-feed-pro.btcturk.com";
            SocketSubscriptionsCombineTarget = 10;
        }

        public BtcTurkSocketClientOptions Copy()
        {
            return Copy<BtcTurkSocketClientOptions>();
        }
    }
}
