using CryptoExchange.Net;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using BtcTurk.Net.Converters;
using BtcTurk.Net.Interfaces;
using BtcTurk.Net.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using BtcTurk.Net.Objects.SocketObjects;
using System.Net.Http;

namespace BtcTurk.Net
{
    public class BtcTurkSocketClient : SocketClient, IBtcTurkSocketClient
    {
        #region Fields
        private static BtcTurkSocketClientOptions defaultOptions = new BtcTurkSocketClientOptions();
        private static BtcTurkSocketClientOptions DefaultOptions => defaultOptions.Copy();
        #endregion

        #region Ctor
        /// <summary>
        /// Create a new instance of BtcTurkSocketClient with default options
        /// </summary>
        public BtcTurkSocketClient() : this(DefaultOptions)
        {
        }

        /// <summary>
        /// Create a new instance of BtcTurkSocketClient using provided options
        /// </summary>
        /// <param name="options">The options to use for this client</param>
        public BtcTurkSocketClient(BtcTurkSocketClientOptions options) : base(options, options.ApiCredentials == null ? null : new BtcTurkAuthenticationProvider(options.ApiCredentials,  ArrayParametersSerialization.MultipleValues))
        {
            Configure(options);
        }
        #endregion

        #region General Methods
        private void Configure(BtcTurkSocketClientOptions options)
        {
        }

        /// <summary>
        /// Set the default options to be used when creating new socket clients
        /// </summary>
        /// <param name="options">The options to use for new clients</param>
        public static void SetDefaultOptions(BtcTurkSocketClientOptions options)
        {
            defaultOptions = options;
        }
        #endregion

        #region Subscriptions
        public CallResult<UpdateSubscription> SubscribeToTicker(string symbol, Action<BtcTurkStreamTickerSingle> onData) => SubscribeToTickerAsync(symbol, onData).Result;
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerAsync(string symbol, Action<BtcTurkStreamTickerSingle> onData)
        {
            var request = new BtcTurkSocketRequest(151, "ticker", symbol, true);
            var internalHandler = new Action<BtcTurkSocketResponse>(data => onData(JsonConvert.DeserializeObject<BtcTurkStreamTickerSingle>(data.Data)));
            return await Subscribe(request.RequestObject(), null, false, internalHandler).ConfigureAwait(false);
        }

        public CallResult<UpdateSubscription> SubscribeToTickers(Action<BtcTurkStreamTickerAll> onData) => SubscribeToTickersAsync(onData).Result;
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickersAsync(Action<BtcTurkStreamTickerAll> onData)
        {
            var request = new BtcTurkSocketRequest(151, "ticker", "all", true);
            var internalHandler = new Action<BtcTurkSocketResponse>(data => onData(JsonConvert.DeserializeObject<BtcTurkStreamTickerAll>(data.Data)));
            return await Subscribe(request.RequestObject(), null, false, internalHandler).ConfigureAwait(false);
        }

        public CallResult<UpdateSubscription> SubscribeToKlines(string symbol, BtcTurkPeriod period, Action<BtcTurkStreamKline> onData) => SubscribeToKlinesAsync(symbol, period, onData).Result;
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlinesAsync(string symbol, BtcTurkPeriod period, Action<BtcTurkStreamKline> onData)
        {
            var request = new BtcTurkSocketRequest(151,"tradeview", $"{symbol}_{JsonConvert.SerializeObject(period, new PeriodEnumConverter(false))}", true);
            var internalHandler = new Action<BtcTurkSocketResponse>(data => onData(JsonConvert.DeserializeObject< BtcTurkStreamKline >(data.Data)));
            return await Subscribe(request.RequestObject(), null, false, internalHandler).ConfigureAwait(false);
        }

        public CallResult<UpdateSubscription> SubscribeToTrades(string symbol, Action<BtcTurkStreamTradeList> onListData, Action<BtcTurkStreamTradeSingle> onRowData) => SubscribeToTradesAsync(symbol, onListData, onRowData).Result;
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradesAsync(string symbol, Action<BtcTurkStreamTradeList> onListData, Action<BtcTurkStreamTradeSingle> onRowData)
        {
            var request = new BtcTurkSocketRequest(151, "trade", symbol, true);
            var internalHandler = new Action<BtcTurkSocketResponse>(data =>
              {
                  if (data.Model == 421) onListData(JsonConvert.DeserializeObject<BtcTurkStreamTradeList>(data.Data));
                  if (data.Model == 422) onRowData(JsonConvert.DeserializeObject<BtcTurkStreamTradeSingle>(data.Data));
              }
            );
            return await Subscribe(request.RequestObject(), null, false, internalHandler).ConfigureAwait(false);
        }

        public CallResult<UpdateSubscription> SubscribeToOrderBookFull(string symbol, Action<BtcTurkStreamOrderBookFull> onData) => SubscribeToOrderBookFullAsync(symbol, onData).Result;
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookFullAsync(string symbol, Action<BtcTurkStreamOrderBookFull> onData)
        {
            var request = new BtcTurkSocketRequest(151, "orderbook", symbol, true);
            var internalHandler = new Action<BtcTurkSocketResponse>(data => onData(JsonConvert.DeserializeObject<BtcTurkStreamOrderBookFull>(data.Data)));
            return await Subscribe(request.RequestObject(), null, false, internalHandler).ConfigureAwait(false);
        }

        public CallResult<UpdateSubscription> SubscribeToOrderBookDiff(string symbol, Action<BtcTurkStreamOrderBookFull> onFullData, Action<BtcTurkStreamOrderBookDifference> onDiffData) => SubscribeToOrderBookDiffAsync(symbol, onFullData, onDiffData).Result;
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookDiffAsync(string symbol, Action<BtcTurkStreamOrderBookFull> onFullData, Action<BtcTurkStreamOrderBookDifference> onDiffData)
        {
            var request = new BtcTurkSocketRequest(151, "obdiff", symbol, true);
            var internalHandler = new Action<BtcTurkSocketResponse>(data =>
            {
                if (data.Model == 431) onFullData(JsonConvert.DeserializeObject<BtcTurkStreamOrderBookFull>(data.Data));
                if (data.Model == 432) onDiffData(JsonConvert.DeserializeObject<BtcTurkStreamOrderBookDifference>(data.Data));
            }
            );
            return await Subscribe(request.RequestObject(), null, false, internalHandler).ConfigureAwait(false);
        }

        #endregion

        #region Private Methods
        protected override SocketConnection GetWebsocket(string address, bool authenticated)
        {
            var socketResult = sockets.Where(s => s.Value.Socket.Url == address && (s.Value.Authenticated == authenticated || !authenticated) && s.Value.Connected).OrderBy(s => s.Value.HandlerCount).FirstOrDefault();
            var result = socketResult.Equals(default(KeyValuePair<int, SocketConnection>)) ? null : socketResult.Value;
            if (result != null)
            {
                if (result.HandlerCount < SocketCombineTarget || (sockets.Count >= MaxSocketConnections && sockets.All(s => s.Value.HandlerCount >= SocketCombineTarget)))
                {
                    // Use existing socket if it has less than target connections OR it has the least connections and we can't make new
                    return result;
                }
            }

            // Create new socket
            var socket = CreateSocket(address);
            // socket.OnMessage += IncomingMessage;

            var socketWrapper = new SocketConnection(this, socket);
            foreach (var kvp in genericHandlers)
                socketWrapper.AddHandler(SocketSubscription.CreateForIdentifier(kvp.Key, false, kvp.Value));

            return socketWrapper;
        }
        
        /*
        private void IncomingMessage(string data)
        {
            System.Diagnostics.Debug.WriteLine( "Message received: " +data);
        }
        */

        protected override bool HandleQueryResponse<T>(SocketConnection s, object request, JToken data, out CallResult<T> callResult)
        {
            // out params
            callResult = new CallResult<T>(default, null);

            var isV2Response = (string)data["op"] == "req";
            if (isV2Response)
            {
                var desResult = Deserialize<T>(data, false);
                if (!desResult.Success)
                {
                    log.Write(LogVerbosity.Warning, $"Failed to deserialize data: {desResult.Error}. Data: {data}");
                    return false;
                }

                callResult = new CallResult<T>(desResult.Data, null);
                return true;
            }

            return true;
        }

        protected override bool HandleSubscriptionResponse(SocketConnection s, SocketSubscription subscription, object request, JToken message, out CallResult<object> callResult)
        {
            // out params
            callResult = new CallResult<object>(null, null);

            // Check Point
            if (message == null || message[0] == null || message.Count() != 2)
                return false;

            int modelNumber = 0;
            var parseResult = int.TryParse(message[0].ToString(), out modelNumber);

            // Check Point
            if (parseResult == false || modelNumber == 0)
                return false;

            // 100: Result for Request
            if (modelNumber == 100)
            {
                try
                {
                    if (request is object[] req)
                    {
                        if (req.Length == 2)
                        {
                            if (req[0] is int && req[1] is BtcTurkSocketRequest)
                            {
                                BtcTurkSocketRequest r = (BtcTurkSocketRequest)req[1];
                                var subResponse = Deserialize<BtcTurkStreamAnswer>(message[1], false);

                                if (subResponse.Data.Message == $"join|{r.Channel}:{r.Event}")
                                {
                                    if (subResponse.Data.OK)
                                    {
                                        log.Write(LogVerbosity.Debug, "Subscription completed");
                                        callResult = new CallResult<object>(subResponse.Data, null);
                                        return true;
                                    }
                                    else
                                    {
                                        log.Write(LogVerbosity.Warning, "Subscription failed: " + subResponse.Error);
                                        callResult = new CallResult<object>(null, subResponse.Error);
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }
                catch
                {
                    return false;
                }
            }

            // 991: Welcome Message
            else if (modelNumber == 991)
                return false;

            return false;
        }

        protected override bool MessageMatchesHandler(JToken message, object request)
        {
            try
            {
                // Check Point
                if (message == null || message[0] == null || message.Count() != 2)
                    return false;

                int modelNumber = 0;
                var parseResult = int.TryParse(message[0].ToString(), out modelNumber);

                // Check Point
                if (parseResult == false || modelNumber == 0)
                    return false;

                // Result
                if (modelNumber == 100)
                    return false;

                // Request
                else if (modelNumber == 101)
                    return false;

                // UserLogin
                else if (modelNumber == 111)
                    return false;

                // UserLoginResult
                else if (modelNumber == 112)
                    return false;

                // UserLogout
                else if (modelNumber == 113)
                    return false;

                // Subscription Request
                else if (modelNumber == 151)
                    return false;

                // BalanceUpdated
                else if (modelNumber == 201)
                    return false;

                // 401: TickerAll
                // 402: TickerPair
                // 421: TradeList
                // 422: TradeSingle
                // 428: TradingView
                // 431: OrderBookFull
                // 432: OrderBookDifference
                else if (modelNumber.IsOneOf(401, 402, 421, 422, 428, 431, 432))
                {
                    var ch = string.Empty;
                    if (modelNumber == 401) ch = "ticker";
                    else if (modelNumber == 402) ch = "ticker";
                    else if (modelNumber == 421) ch = "trade";
                    else if (modelNumber == 422) ch = "trade";
                    else if (modelNumber == 428) ch = "tradeview";
                    else if (modelNumber == 431) ch = "orderbook";
                    else if (modelNumber == 432) ch = "obdiff";
                    if (request is object[] req)
                    {
                        if (req.Length == 2)
                        {
                            if (req[0] is int && req[1] is BtcTurkSocketRequest)
                            {
                                BtcTurkSocketRequest r = (BtcTurkSocketRequest)req[1];
                                if (r.Type == 151
                                    && (modelNumber.IsOneOf(431, 432) ? r.Channel.IsOneOf("orderbook", "obdiff") : r.Channel == ch)
                                    && r.Event == message[1]["event"].ToString()
                                    && r.Join)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }

                // UserOrderMatch
                else if (modelNumber == 441)
                    return false;

                // OrderInsert
                else if (modelNumber == 451)
                    return false;

                // OrderDelete
                else if (modelNumber == 452)
                    return false;

                // OrderUpdate
                else if (modelNumber == 453)
                    return false;

                // Welcome Message
                else if (modelNumber == 991)
                    return false;
            }
            catch { }

            return false;
        }

        protected override bool MessageMatchesHandler(JToken message, string identifier)
        {
            if (message.Type != JTokenType.Object)
                return false;

            return false;
        }

        protected override async Task<CallResult<bool>> AuthenticateSocket(SocketConnection s)
        {
            if (authProvider == null)
                return new CallResult<bool>(false, new NoApiCredentialsError());

            var authParams = authProvider.AddAuthenticationToParameters(""/*baseAddressAuthenticated*/, HttpMethod.Get, new Dictionary<string, object>(), true, PostParameters.InBody, ArrayParametersSerialization.Array);
            var authObjects = new BtcTurkAuthenticationRequest
            {
                AccessKeyId = authProvider.Credentials.Key.GetString(),
                Operation = "auth",
                SignatureMethod = (string)authParams["SignatureMethod"],
                SignatureVersion = authParams["SignatureVersion"].ToString(),
                Timestamp = (string)authParams["Timestamp"],
                Signature = (string)authParams["Signature"]
            };

            CallResult<bool> result = new CallResult<bool>(false, new ServerError("No response from server"));
            await s.SendAndWait(authObjects, ResponseTimeout, data =>
            {
                if ((string)data["op"] != "auth")
                    return false;

                /*
                var authResponse = Deserialize<BtcTurkSocketAuthDataResponse<object>>(data, false);
                if (!authResponse.Success)
                {
                    log.Write(LogVerbosity.Warning, "Authorization failed: " + authResponse.Error);
                    result = new CallResult<bool>(false, authResponse.Error);
                    return true;
                }
                if (!authResponse.Data.IsSuccessful)
                {
                    log.Write(LogVerbosity.Warning, "Authorization failed: " + authResponse.Data.ErrorMessage);
                    result = new CallResult<bool>(false, new ServerError(authResponse.Data.ErrorCode, authResponse.Data.ErrorMessage));
                    return true;
                }
                */

                log.Write(LogVerbosity.Debug, "Authorization completed");
                result = new CallResult<bool>(true, null);
                return true;
            });

            return result;
        }

        protected override async Task<bool> Unsubscribe(SocketConnection connection, SocketSubscription s)
        {
            // string topic = "";
            object unsub = null;
            string unsubId = null;
            string idField = "id";
            /*
            if (s.Request is BtcTurkSubscribeRequest hRequest)
            {
                topic = hRequest.Topic;
                unsubId = NextId().ToString();
                unsub = new BtcTurkUnsubscribeRequest(unsubId, topic);
            }

            if (s.Request is BtcTurkAuthenticatedRequest haRequest)
            {
                topic = haRequest.Topic;
                unsubId = NextId().ToString();
                unsub = new BtcTurkAuthUnsubscribeRequest(unsubId, topic);
                idField = "cid";
            }
            */

            var result = false;
            await connection.SendAndWait(unsub, ResponseTimeout, data =>
            {
                if (data.Type != JTokenType.Object)
                    return false;

                var id = (string)data[idField];
                if (id == unsubId)
                {
                    result = (string)data["status"] == "ok";
                    return true;
                }

                return false;
            });
            return result;
        }
        #endregion

    }
}
