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
using Microsoft.Extensions.Logging;

namespace BtcTurk.Net
{
    public class BtcTurkSocketClient : SocketClient, IBtcTurkSocketClient
    {
        #region Fields
        protected static BtcTurkSocketClientOptions defaultOptions = new BtcTurkSocketClientOptions();
        protected static BtcTurkSocketClientOptions DefaultOptions => defaultOptions.Copy();
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
        public BtcTurkSocketClient(BtcTurkSocketClientOptions options) : base("BtcTurk", options, options.ApiCredentials == null ? null : new BtcTurkAuthenticationProvider(options.ApiCredentials, ArrayParametersSerialization.MultipleValues))
        {
            Configure(options);
        }
        #endregion

        #region General Methods
        protected virtual void Configure(BtcTurkSocketClientOptions options)
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
        public virtual CallResult<UpdateSubscription> SubscribeToTicker(string symbol, Action<BtcTurkStreamTickerSingle> onData) => SubscribeToTickerAsync(symbol, onData).Result;
        public virtual async Task<CallResult<UpdateSubscription>> SubscribeToTickerAsync(string symbol, Action<BtcTurkStreamTickerSingle> onData)
        {
            var request = new BtcTurkSocketRequest(151, "ticker", symbol, true);
            var internalHandler = new Action<DataEvent< BtcTurkSocketResponse>>(data => onData(JsonConvert.DeserializeObject<BtcTurkStreamTickerSingle>(data.Data.Data)));
            return await SubscribeAsync(request.RequestObject(), null, false, internalHandler).ConfigureAwait(false);
        }

        public virtual CallResult<UpdateSubscription> SubscribeToTickers(Action<BtcTurkStreamTickerAll> onData) => SubscribeToTickersAsync(onData).Result;
        public virtual async Task<CallResult<UpdateSubscription>> SubscribeToTickersAsync(Action<BtcTurkStreamTickerAll> onData)
        {
            var request = new BtcTurkSocketRequest(151, "ticker", "all", true);
            var internalHandler = new Action<DataEvent<BtcTurkSocketResponse>>(data => onData(JsonConvert.DeserializeObject<BtcTurkStreamTickerAll>(data.Data.Data)));
            return await SubscribeAsync(request.RequestObject(), null, false, internalHandler).ConfigureAwait(false);
        }

        public virtual CallResult<UpdateSubscription> SubscribeToKlines(string symbol, BtcTurkPeriod period, Action<BtcTurkStreamKline> onData) => SubscribeToKlinesAsync(symbol, period, onData).Result;
        public virtual async Task<CallResult<UpdateSubscription>> SubscribeToKlinesAsync(string symbol, BtcTurkPeriod period, Action<BtcTurkStreamKline> onData)
        {
            var request = new BtcTurkSocketRequest(151, "tradeview", $"{symbol}_{JsonConvert.SerializeObject(period, new PeriodEnumConverter(false))}", true);
            var internalHandler = new Action<DataEvent<BtcTurkSocketResponse>>(data => onData(JsonConvert.DeserializeObject<BtcTurkStreamKline>(data.Data.Data)));
            return await SubscribeAsync(request.RequestObject(), null, false, internalHandler).ConfigureAwait(false);
        }

        public virtual CallResult<UpdateSubscription> SubscribeToTrades(string symbol, Action<BtcTurkStreamTradeList> onListData, Action<BtcTurkStreamTradeSingle> onRowData) => SubscribeToTradesAsync(symbol, onListData, onRowData).Result;
        public virtual async Task<CallResult<UpdateSubscription>> SubscribeToTradesAsync(string symbol, Action<BtcTurkStreamTradeList> onListData, Action<BtcTurkStreamTradeSingle> onRowData)
        {
            var request = new BtcTurkSocketRequest(151, "trade", symbol, true);
            var internalHandler = new Action<DataEvent<BtcTurkSocketResponse>>(data =>
              {
                  if (data.Data.Model == 421) onListData(JsonConvert.DeserializeObject<BtcTurkStreamTradeList>(data.Data.Data));
                  if (data.Data.Model == 422) onRowData(JsonConvert.DeserializeObject<BtcTurkStreamTradeSingle>(data.Data.Data));
              }
            );
            return await SubscribeAsync(request.RequestObject(), null, false, internalHandler).ConfigureAwait(false);
        }

        public virtual CallResult<UpdateSubscription> SubscribeToOrderBookFull(string symbol, Action<BtcTurkStreamOrderBookFull> onData) => SubscribeToOrderBookFullAsync(symbol, onData).Result;
        public virtual async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookFullAsync(string symbol, Action<BtcTurkStreamOrderBookFull> onData)
        {
            var request = new BtcTurkSocketRequest(151, "orderbook", symbol, true);
            var internalHandler = new Action<DataEvent<BtcTurkSocketResponse>>(data => onData(JsonConvert.DeserializeObject<BtcTurkStreamOrderBookFull>(data.Data.Data)));
            return await SubscribeAsync(request.RequestObject(), null, false, internalHandler).ConfigureAwait(false);
        }

        public virtual CallResult<UpdateSubscription> SubscribeToOrderBookDiff(string symbol, Action<BtcTurkStreamOrderBookFull> onFullData, Action<BtcTurkStreamOrderBookDifference> onDiffData) => SubscribeToOrderBookDiffAsync(symbol, onFullData, onDiffData).Result;
        public virtual async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookDiffAsync(string symbol, Action<BtcTurkStreamOrderBookFull> onFullData, Action<BtcTurkStreamOrderBookDifference> onDiffData)
        {
            var request = new BtcTurkSocketRequest(151, "obdiff", symbol, true);
            var internalHandler = new Action<DataEvent<BtcTurkSocketResponse>>(data =>
            {
                if (data.Data.Model == 431) onFullData(JsonConvert.DeserializeObject<BtcTurkStreamOrderBookFull>(data.Data.Data));
                if (data.Data.Model == 432) onDiffData(JsonConvert.DeserializeObject<BtcTurkStreamOrderBookDifference>(data.Data.Data));
            }
            );
            return await SubscribeAsync(request.RequestObject(), null, false, internalHandler).ConfigureAwait(false);
        }

        #endregion

        #region Private Methods
        protected override SocketConnection GetSocketConnection(string address, bool authenticated)
        {
            return this.BtcTurkSocketConnection(address, authenticated);
        }
        protected virtual SocketConnection BtcTurkSocketConnection(string address, bool authenticated)
        {
            var socketResult = sockets.Where(s => s.Value.Socket.Url == address && (s.Value.Authenticated == authenticated || !authenticated) && s.Value.Connected).OrderBy(s => s.Value.SubscriptionCount).FirstOrDefault();
            var result = socketResult.Equals(default(KeyValuePair<int, SocketConnection>)) ? null : socketResult.Value;
            if (result != null)
            {
                if (result.SubscriptionCount < SocketCombineTarget || (sockets.Count >= MaxSocketConnections && sockets.All(s => s.Value.SubscriptionCount >= SocketCombineTarget)))
                {
                    // Use existing socket if it has less than target connections OR it has the least connections and we can't make new
                    return result;
                }
            }

            // Create new socket
            var socket = CreateSocket(address);
            var socketConnection = new SocketConnection(this, socket);
            socketConnection.UnhandledMessage += HandleUnhandledMessage;
            foreach (var kvp in genericHandlers)
            {
                var handler = SocketSubscription.CreateForIdentifier(NextId(), kvp.Key, false, kvp.Value);
                socketConnection.AddSubscription(handler);
            }

            return socketConnection;
        }

        protected override bool HandleQueryResponse<T>(SocketConnection s, object request, JToken data, out CallResult<T> callResult)
        {
            return this.BtcTurkHandleQueryResponse<T>(s, request, data, out callResult);
        }
        protected virtual bool BtcTurkHandleQueryResponse<T>(SocketConnection s, object request, JToken data, out CallResult<T> callResult)
        {
            // out params
            callResult = new CallResult<T>(default, null);

            var isV2Response = (string)data["op"] == "req";
            if (isV2Response)
            {
                var desResult = Deserialize<T>(data, false);
                if (!desResult.Success)
                {
                    log.Write(LogLevel.Warning, $"Failed to deserialize data: {desResult.Error}. Data: {data}");
                    return false;
                }

                callResult = new CallResult<T>(desResult.Data, null);
                return true;
            }

            return true;
        }

        protected override bool HandleSubscriptionResponse(SocketConnection s, SocketSubscription subscription, object request, JToken message, out CallResult<object> callResult)
        {
            return this.BtcTurkHandleSubscriptionResponse(s, subscription, request, message, out callResult);
        }
        protected virtual bool BtcTurkHandleSubscriptionResponse(SocketConnection s, SocketSubscription subscription, object request, JToken message, out CallResult<object> callResult)
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
                                        log.Write(LogLevel.Debug, "Subscription completed");
                                        callResult = new CallResult<object>(subResponse.Data, null);
                                        return true;
                                    }
                                    else
                                    {
                                        log.Write(LogLevel.Warning, "Subscription failed: " + subResponse.Error);
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
            return this.BtcTurkMessageMatchesHandler(message, request);
        }
        protected virtual bool BtcTurkMessageMatchesHandler(JToken message, object request)
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
            return this.BtcTurkMessageMatchesHandler(message, identifier);
        }
        protected virtual bool BtcTurkMessageMatchesHandler(JToken message, string identifier)
        {
            if (message.Type != JTokenType.Object)
                return false;

            return false;
        }

        protected override async Task<CallResult<bool>> AuthenticateSocketAsync(SocketConnection s)
        {
            return await this.BtcTurkAuthenticateSocket(s);
        }
        protected virtual async Task<CallResult<bool>> BtcTurkAuthenticateSocket(SocketConnection s)
        {
            if (authProvider == null)
                return new CallResult<bool>(false, new NoApiCredentialsError());

            var authParams = authProvider.AddAuthenticationToParameters(""/*baseAddressAuthenticated*/, HttpMethod.Get, new Dictionary<string, object>(), true, HttpMethodParameterPosition.InBody, ArrayParametersSerialization.Array);
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
            await s.SendAndWaitAsync(authObjects, ResponseTimeout, data =>
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

                log.Write(LogLevel.Debug, "Authorization completed");
                result = new CallResult<bool>(true, null);
                return true;
            });

            return result;
        }

        protected override async Task<bool> UnsubscribeAsync(SocketConnection connection, SocketSubscription s)
        {
            return await this.BtcTurkUnsubscribe(connection, s);
        }
        protected virtual async Task<bool> BtcTurkUnsubscribe(SocketConnection connection, SocketSubscription s)
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
            await connection.SendAndWaitAsync(unsub, ResponseTimeout, data =>
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
