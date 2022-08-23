using BtcTurk.Net;
using BtcTurk.Net.Objects.ClientObjects;
using BtcTurk.Net.Objects.SocketObjects;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BtcTurk.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            // var ws = new BtcTurkSocketClient(new BtcTurkSocketClientOptions { LogLevel = LogLevel.Debug });
            // var login = ws.Login("info@burakoner.com", "eyJhbGciOiJSUzI1NiIsImtpZCI6IkZDODQ4MjRCOEIyMDc3ODRGMTJGNTdCMTlCOEMyMTVCQTczREM0RjUiLCJ0eXAiOiJKV1QiLCJ4NXQiOiJfSVNDUzRzZ2Q0VHhMMWV4bTR3aFc2Yzl4UFUifQ.eyJuYmYiOjE2MzM4ODYzMDgsImV4cCI6MTYzMzg4OTkwOCwiaXNzIjoiaHR0cHM6Ly9zc28uYnRjdHVyay5jb20iLCJhdWQiOlsiaHR0cHM6Ly9zc28uYnRjdHVyay5jb20vcmVzb3VyY2VzIiwiQnRjVHJhZGVyQXBpIiwia3ljLWFwaSJdLCJjbGllbnRfaWQiOiJidGN0dXJrIiwic3ViIjoiMjUyMjI1IiwiYXV0aF90aW1lIjoxNjMzODg2MzA3LCJpZHAiOiJsb2NhbCIsIkFzcE5ldC5JZGVudGl0eS5TZWN1cml0eVN0YW1wIjoiUkxXSFoyS0UyNFlZSFJHVTVVNFFTWVVFWU9FN0VCVFciLCJwcmVmZXJyZWRfdXNlcm5hbWUiOiJpbmZvQGJ1cmFrb25lci5jb20iLCJlbWFpbCI6ImluZm9AYnVyYWtvbmVyLmNvbSIsImVtYWlsX3ZlcmlmaWVkIjp0cnVlLCJwaG9uZV9udW1iZXIiOiI1MzIzNTAwNjgyIiwicGhvbmVfbnVtYmVyX3ZlcmlmaWVkIjp0cnVlLCJ0ZmEiOiIxIiwibmFtZSI6IkJVUkFLIMOWTkVSIiwiYnJva2VyX2lkIjoiMSIsImxvY2FsX3VzZXIiOnRydWUsIm1lbWJlcl90eXBlIjoiRGVmYXVsdCIsImRldmljZV9pZCI6ImZlOWU0ZWRhMDgyNTdmZjFiZjQ5OTA2NDFhNGEyOWRjIiwic2NvcGUiOlsib3BlbmlkIiwib2ZmbGluZV9hY2Nlc3MiLCJreWMiXSwiYW1yIjpbInB3ZCJdfQ.QFIiPhUalxNV4XZCb2KgWDTcb3MXWfmTk660RtpOxRzTx6FlEZRdYoHbVELC_U_HYEDUlaeJbJu0rXHUsJmG95k-0SBJzV5RWs_oG_v9O8rV-e0JCh4zKT3p6-9wGG-kkSyvYEZ0Ud9ANn4BNemSRnPdiXGr2SL-RBuLpteDFKCVSBO_MdxwTP4OEiZdLPoPV10Ri4yZ6jpQzrovY_iG1WBGzPdnAGWlnpUougQwLFQPdDIL6DGaet0P7XMw3lkmK5UGSUnQD7rkDJWJT-xV2fpgHyButZH-Dcc7fP-AQM6RaglWMvkRaSlD2CqvHFe-6UbKE-ndwwIjgUOcJlbx2g").Result;

            var pairs = new List<string> {
                "ATOMTRY","BTCTRY","DASHTRY","EOSTRY","ETHTRY","LINKTRY","LTCTRY","NEOTRY","USDTTRY","XLMTRY","XRPTRY","XTZTRY",
                "ATOMUSDT","BTCUSDT","DASHUSDT","EOSUSDT","ETHUSDT","LINKUSDT","LTCUSDT","NEOUSDT","XLMUSDT","XRPUSDT","XTZUSDT",
                "ATOMBTC","DASHBTC","EOSBTC","ETHBTC","LINKBTC","LTCBTC","NEOBTC","XLMBTC","XRPBTC","XTZBTC"
            };

            /* BtcTurkClient Object */
            BtcTurkClient apiClient = new BtcTurkClient();

            // Public Api Endpoints:
            var btc00 = apiClient.GetServerVersion();
            var btc01 = apiClient.GetServerTime();
            var btc02 = apiClient.GetServerVersion();
            var btc03 = apiClient.GetServerExchangeInfo();
            var btc04 = apiClient.GetServerPing();
            var btc05 = apiClient.GetTicker("BTCTRY");
            var btc06 = apiClient.GetTradesV2("BTCTRY");
            var btc07 = apiClient.GetPriceGraphHistory("BTCTRY", Net.Objects.BtcTurkPeriod.OneHour);
            // ...

            // Private Api Endpoints:
            apiClient.SetApiCredentials("XXXXXXXX-API-KEY-XXXXXXXX", "XXXXXXXX-API-SECRET-XXXXXXXX");
            var btc11 = apiClient.GetBalances();
            var btc12 = apiClient.PlaceOrder("BTCTRY", 1.0m, Net.Objects.BtcTurkOrderSide.Buy, Net.Objects.BtcTurkOrderMethod.Market);
            // ...

            Console.ReadLine();

            /* BtcTurkSocketClient Object */
            var sock = new BtcTurkSocketClient(new BtcTurkSocketClientOptions { LogLevel = LogLevel.Debug });

            /* Public Socket Endpoints: */
            var subs = new List<UpdateSubscription>();

            // Single Ticker
            /**/
            {
                var subscription = sock.SubscribeToTicker("BTCTRY", (ticker) =>
                {
                    if (ticker != null)
                    {
                        Console.WriteLine($"Channel: {ticker.Channel} Event:{ticker.Event} Type:{ticker.Type} >> " +
                        $"B:{ticker.Bid} A:{ticker.Ask} PS:{ticker.PairSymbol} NS:{ticker.NumeratorSymbol} DS:{ticker.DenominatorSymbol} " +
                        $"O:{ticker.Open} H:{ticker.High} L:{ticker.Low} LA:{ticker.Close} V:{ticker.Volume} " +
                        $"AV:{ticker.AveragePrice} D:{ticker.DailyChange} DP:{ticker.DailyChangePercent} PId:{ticker.PairId} Ord:{ticker.OrderNum} ");
                    }
                });
                subs.Add(subscription.Data);
            }
            Console.ReadLine();
            /**/

            // All Tickers
            /**/
            {
                var subscription = sock.SubscribeToTickers((data) =>
                {
                    if (data != null)
                    {
                        foreach (var ticker in data.Items)
                        {
                            Console.WriteLine($"Channel: {data.Channel} Event:{data.Event} Type:{data.Type} >> " +
                                $"B:{ticker.Bid} A:{ticker.Ask} PS:{ticker.PairSymbol} NS:{ticker.NumeratorSymbol} DS:{ticker.DenominatorSymbol} " +
                                $"O:{ticker.Open} H:{ticker.High} L:{ticker.Low} LA:{ticker.Close} V:{ticker.Volume} " +
                                $"AV:{ticker.AveragePrice} D:{ticker.DailyChange} DP:{ticker.DailyChangePercent} PId:{ticker.PairId} Ord:{ticker.OrderNum} ");
                        }
                    }
                });
                subs.Add(subscription.Data);
            }
            /**/

            // Klines
            /**/
            foreach (var pair in pairs)
            {
                var subscription = sock.SubscribeToKlines(pair, Net.Objects.BtcTurkPeriod.OneMinute, (data) =>
                {
                    if (data != null)
                    {
                        Console.WriteLine($"Channel: {data.Channel} Event:{data.Event} Type:{data.Type} >> D:{data.Date} S:{data.Pair} P:{data.Period} O:{data.Open} H:{data.High} L:{data.Low} V:{data.Close} V:{data.Volume}");
                    }
                });
                subs.Add(subscription.Data);
            }
            /**/

            // Single Trade
            /**/
            {
                var subscription = sock.SubscribeToTrades("BTCTRY", (trades) =>
                {
                    if (trades != null)
                    {
                        foreach (var trade in trades.Items)
                        {
                            Console.WriteLine($"Channel: {trades.Channel} [List] Event:{trades.Event} Type:{trades.Type} PairSymbol:{trades.PairSymbol} >> " +
                                $"D:{trade.Time} I:{trade.TradeId} A:{trade.Amount} A:{trade.Price} PS:{trade.PairSymbol} S:{trade.S} ");
                        }
                    }
                }, (trade) =>
                {
                    if (trade != null)
                    {
                        Console.WriteLine($"Channel: {trade.Channel} [Row] Event:{trade.Event} Type:{trade.Type} PairSymbol:{trade.PairSymbol} >> " +
                        $"D:{trade.Time} I:{trade.TradeId} A:{trade.Amount} A:{trade.Price} PS:{trade.PairSymbol} S:{trade.S} ");
                    }
                });
                subs.Add(subscription.Data);
            }
            /**/

            // All Pairs Trades
            /**/
            foreach (var pair in pairs)
            {
                var subscription = sock.SubscribeToTrades(pair, (trades) =>
                {
                    if (trades != null)
                    {
                        foreach (var trade in trades.Items)
                        {
                            Console.WriteLine($"Channel: {trades.Channel} [List] Event:{trades.Event} Type:{trades.Type} PairSymbol:{trades.PairSymbol} >> " +
                                $"D:{trade.Time} I:{trade.TradeId} A:{trade.Amount} A:{trade.Price} PS:{trade.PairSymbol} S:{trade.S} ");
                        }
                    }
                }, (trade) =>
                {
                    if (trade != null)
                    {
                        Console.WriteLine($"Channel: {trade.Channel} [Row] Event:{trade.Event} Type:{trade.Type} PairSymbol:{trade.PairSymbol} >> " +
                            $"D:{trade.Time} I:{trade.TradeId} A:{trade.Amount} A:{trade.Price} PS:{trade.PairSymbol} S:{trade.S} ");
                    }
                });
                subs.Add(subscription.Data);
            }
            /**/

            /**/
            // Order Book Full
            foreach (var pair in pairs)
            {
                var subscription = sock.SubscribeToOrderBookFull(pair, (data) =>
                {
                    if (data != null)
                    {
                        Console.WriteLine($"Channel: {data.Channel} [Full] Event:{data.Event} Type:{data.Type} PairSymbol:{data.PairSymbol} >> ");
                        for (var i = 0; i < Math.Min(data.Asks.Count(), 5); i++)
                        {
                            Console.WriteLine($"Ask {i + 1} -> Price: {data.Asks[i].Price} Amount:{data.Asks[i].Amount}");
                        }
                        for (var i = 0; i < Math.Min(data.Bids.Count(), 5); i++)
                        {
                            Console.WriteLine($"Bid {i + 1} -> Price: {data.Bids[i].Price} Amount:{data.Bids[i].Amount}");
                        }
                    }
                });
                subs.Add(subscription.Data);
            }
            /**/

            // Order Book Difference
            /**/
            foreach (var pair in pairs)
            {
                var subscription = sock.SubscribeToOrderBookDiff("BTCTRY", (data) =>
                {
                    if (data != null)
                    {
                        Console.WriteLine($"Channel: {data.Channel} [Full] Event:{data.Event} Type:{data.Type} PairSymbol:{data.PairSymbol} >> ");
                        for (var i = 0; i < Math.Min(data.Asks.Count(), 5); i++)
                        {
                            Console.WriteLine($"Ask {i + 1} -> Price: {data.Asks[i].Price} Amount:{data.Asks[i].Amount}");
                        }
                        for (var i = 0; i < Math.Min(data.Bids.Count(), 5); i++)
                        {
                            Console.WriteLine($"Bid {i + 1} -> Price: {data.Bids[i].Price} Amount:{data.Bids[i].Amount}");
                        }
                    }
                }, (data) =>
                {
                    if (data != null)
                    {
                        Console.WriteLine($"Channel: {data.Channel} [Diff] Event:{data.Event} Type:{data.Type} PairSymbol:{data.PairSymbol} >> ");
                        for (var i = 0; i < Math.Min(data.Asks.Count(), 5); i++)
                        {
                            Console.WriteLine($"Ask {i + 1} -> Price: {data.Asks[i].Price} Amount:{data.Asks[i].Amount}");
                        }
                        for (var i = 0; i < Math.Min(data.Bids.Count(), 5); i++)
                        {
                            Console.WriteLine($"Bid {i + 1} -> Price: {data.Bids[i].Price} Amount:{data.Bids[i].Amount}");
                        }
                    }
                });
                subs.Add(subscription.Data);
            }
            /**/

            Console.ReadLine();

        }
    }
}