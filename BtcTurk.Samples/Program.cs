using BtcTurk.Net;
using BtcTurk.Net.Objects.Core;
using BtcTurk.Net.Objects.RestApi;
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
            var btc07 = apiClient.GetPriceGraphHistory("BTCTRY", BtcTurkPeriod.OneHour);
            // ...

            // Private Api Endpoints:
            apiClient.SetApiCredentials("XXXXXXXX-API-KEY-XXXXXXXX", "XXXXXXXX-API-SECRET-XXXXXXXX");
            var btc11 = apiClient.GetBalances();
            var btc12 = apiClient.PlaceOrder("BTCTRY", 1.0m, BtcTurkOrderSide.Buy, BtcTurkOrderMethod.Market);
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
                var subscription = sock.SubscribeToKlines(pair, BtcTurkPeriod.OneMinute, (data) =>
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