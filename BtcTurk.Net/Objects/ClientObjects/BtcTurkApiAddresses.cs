using System;
using System.Collections.Generic;
using System.Text;

namespace BtcTurk.Net.Objects.ClientObjects
{
    public class BtcTurkApiAddresses
    {
        public string ApiAddress { get; set; }
        public string WebsocketAddress { get; set; }

        public static BtcTurkApiAddresses Default = new BtcTurkApiAddresses
        {
            ApiAddress = "https://api.btcturk.com/api",
            WebsocketAddress = "wss://ws-feed-pro.btcturk.com"
            // WebsocketAddress = "wss://ws-feed-sandbox.btctrader.com";
        };
    }
}