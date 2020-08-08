using BtcTurk.Net;
using BtcTurk.Net.Objects.SocketObjects;
using CryptoExchange.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BtcTurk.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            // Variables
            var dictOrderBook = new Dictionary<string, BtcTurkStreamOrderBookFull>();
            var dictTickers = new Dictionary<string, BtcTurkStreamTickerRow>();

            // Objects
            var sock = new BtcTurkSocketClient();
            var cli = new BtcTurkClient();
            cli.SetApiCredentials("APIKEY", "APISECRET");

            // Order Book Full
            var pairs = new List<string> {
                "ATOMTRY","BTCTRY","DASHTRY","EOSTRY","ETHTRY","LINKTRY","LTCTRY","NEOTRY","USDTTRY","XLMTRY","XRPTRY","XTZTRY",
                "ATOMUSDT","BTCUSDT","DASHUSDT","EOSUSDT","ETHUSDT","LINKUSDT","LTCUSDT","NEOUSDT","XLMUSDT","XRPUSDT","XTZUSDT",
                "ATOMBTC","DASHBTC","EOSBTC","ETHBTC","LINKBTC","LTCBTC","NEOBTC","XLMBTC","XRPBTC","XTZBTC"
            };
            foreach (var pair in pairs)
            {
                sock.SubscribeToOrderBookFull(pair, (data) =>
                {
                    if (data != null)
                    {
                        dictOrderBook[pair] = data;
                    }
                });
            }

            // All Tickers
            sock.SubscribeToTickers((data) =>
            {
                if (data != null)
                {
                    foreach (var ticker in data.Items)
                    {
                        dictTickers[ticker.PairSymbol] = ticker;

                        Console.WriteLine($"Channel: {data.Channel} Event:{data.Event} Type:{data.Type} >> " +
                            $"B:{ticker.Bid} A:{ticker.Ask} PS:{ticker.PairSymbol} NS:{ticker.NumeratorSymbol} DS:{ticker.DenominatorSymbol} " +
                            $"O:{ticker.Open} H:{ticker.High} L:{ticker.Low} LA:{ticker.Close} V:{ticker.Volume} " +
                            $"AV:{ticker.AveragePrice} D:{ticker.DailyChange} DP:{ticker.DailyChangePercent} PId:{ticker.PairId} Ord:{ticker.OrderNum} ");

                        var algorithm = false;
                        // TODO: Trade Algorithm

                        if (algorithm)
                        {
                            if (dictOrderBook.ContainsKey(ticker.PairSymbol))
                            {
                                var order = cli.PlaceOrder(
                                    pairSymbol: ticker.PairSymbol,
                                    price: dictOrderBook[ticker.PairSymbol].Asks[0].Price,
                                    quantity: dictOrderBook[ticker.PairSymbol].Asks[0].Amount,
                                    orderSide: Net.Objects.BtcTurkOrderSide.Buy,
                                    orderMethod: Net.Objects.BtcTurkOrderMethod.Market
                                    );
                            }
                        }

                    }
                }
            });

            /**/



            Console.ReadLine();



            Console.ReadLine();
#if AAA


            var pairs = new List<string> {
                "ATOMTRY","BTCTRY","DASHTRY","EOSTRY","ETHTRY","LINKTRY","LTCTRY","NEOTRY","USDTTRY","XLMTRY","XRPTRY","XTZTRY",
                "ATOMUSDT","BTCUSDT","DASHUSDT","EOSUSDT","ETHUSDT","LINKUSDT","LTCUSDT","NEOUSDT","XLMUSDT","XRPUSDT","XTZUSDT",
                "ATOMBTC","DASHBTC","EOSBTC","ETHBTC","LINKBTC","LTCBTC","NEOBTC","XLMBTC","XRPBTC","XTZBTC"
            };

            /* BtcTurkSocketClient Object */
            var sock = new BtcTurkSocketClient(new BtcTurkSocketClientOptions { LogVerbosity = CryptoExchange.Net.Logging.LogVerbosity.Debug });

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


            var cliOptions = new BtcTurk.Net.BtcTurkClientOptions
            {
                // Rate Limiters
                RateLimiters = new List<CryptoExchange.Net.Interfaces.IRateLimiter>
                {
                    // 60 Saniyede 1000 Request
                    new CryptoExchange.Net.RateLimiter.RateLimiterTotal(600, TimeSpan.FromMilliseconds(60*1000)),

                    // 15 Saniyede 100 Request
                    new CryptoExchange.Net.RateLimiter.RateLimiterTotal(100, TimeSpan.FromSeconds(15)),
                },

                // Rate Limiting Behaviour
                RateLimitingBehaviour = CryptoExchange.Net.Objects.RateLimitingBehaviour.Wait,
            };
            BtcTurk.Net.BtcTurkClient.SetDefaultOptions(cliOptions);


#endif
        }
    }
}