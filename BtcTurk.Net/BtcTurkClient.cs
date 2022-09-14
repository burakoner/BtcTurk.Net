using BtcTurk.Net.Converters;
using BtcTurk.Net.Helpers;
using BtcTurk.Net.Objects;
using BtcTurk.Net.Objects.ClientObjects;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BtcTurk.Net
{
    public class BtcTurkClient : BaseRestClient
    {
        #region Internal Fields
        internal BtcTurkClientOptions Options { get; }
        internal BtcTurkClientSingleApi SingleApi { get; }
        #endregion

        #region Endpoints
        // Api Version
        protected const string V1 = "1";
        protected const string V2 = "2";
        protected const string NoVersion = "";
        protected const string PublicVersion = "2";
        protected const string SignedVersion = "2";

        // Methods
        protected const string GetMethod = "GET";
        protected const string PostMethod = "POST";
        protected const string DeleteMethod = "DELETE";
        protected const string PutMethod = "PUT";

        // V1 Endpoints
        protected const string V1_Balances_Endpoint = "users/balances";
        protected const string V1_Order_Endpoint = "order";
        protected const string V1_AllOrders_Endpoint = "allOrders";
        protected const string V1_OpenOrders_Endpoint = "openOrders";
        protected const string V1_Trades_Endpoint = "trades";
        protected const string V1_TransactionsTrade_Endpoint = "users/transactions/trade";
        protected const string V1_TransactionsTradeByPair_Endpoint = "users/transactions/trade/{pairSymbol}";
        protected const string V1_TransactionsCrypto_Endpoint = "users/transactions/crypto";
        protected const string V1_TransactionsFiat_Endpoint = "users/transactions/fiat";

        // Account
        protected const string Account_OtpQrCode_Endpoint = "account/otp-qr-code";
        protected const string Account_OtpGetHash_Endpoint = "account/otp-get-hash";
        protected const string Account_OtpSms_Endpoint = "account/otp-sms";
        protected const string Account_UsersInfo_Endpoint = "users/info";
        protected const string Account_Info_Endpoint = "account/info";
        protected const string Account_ChangePassword_Endpoint = "account/change-password";
        protected const string Account_UsersLog_Endpoint = "users/logs";
        protected const string Account_AccountLogs_Endpoint = "account/logs";
        protected const string Account_AccountProfile_Endpoint = "account/profile";
        protected const string Account_UsersCommissions_Endpoint = "users/commissions";
        protected const string Account_Commissions_Endpoint = "account/commissions";
        protected const string Account_UsersLimits_Endpoint = "users/limits";
        protected const string Account_Limits_Endpoint = "account/limits";
        protected const string Account_SmsCode_Endpoint = "account/sms-code";
        protected const string Account_Notification_Endpoint = "account/notification";
        protected const string Account_UsersBalances_Endpoint = "users/balances";
        protected const string Account_Balances_Endpoint = "account/balances";
        protected const string Account_Timeout_Endpoint = "account/timeout";
        protected const string Account_History_Endpoint = "account/history";
        protected const string Account_AdvancedOrderHistory_Endpoint = "account/advanced-order-history";

        // AccountAuth
        protected const string AccountAuth_Login_Endpoint = "auth/login";
        protected const string AccountAuth_Otp_Endpoint = "auth/otp";
        protected const string AccountAuth_OtpQrCode_Endpoint = "auth/otp-qr-code";
        protected const string AccountAuth_ResetPassword_Endpoint = "auth/reset-password";

        // AccountBank
        protected const string AccountBank_AccountBanks_Endpoint = "account/banks";
        protected const string AccountBank_AccountBanksId_Endpoint = "account/banks/{id}";

        // AccountTransaction
        protected const string AccountTransaction_UsersTransactionsTrade_Endpoint = "users/transactions/trade";
        protected const string AccountTransaction_UsersTransactionsTradePairSymbol_Endpoint = "users/transactions/trade/{pairSymbol}";
        protected const string AccountTransaction_AccountTransactionsTrade_Endpoint = "account/transactions/trade";
        protected const string AccountTransaction_AccountTransactionsTradePairSymbol_Endpoint = "account/transactions/trade/{pairSymbol}";
        protected const string AccountTransaction_UsersTransactionsCrypto_Endpoint = "users/transactions/crypto";
        protected const string AccountTransaction_AccountTransactionsCrypto_Endpoint = "account/transactions/crypto";
        protected const string AccountTransaction_UsersTransactionsFiat_Endpoint = "users/transactions/fiat";
        protected const string AccountTransaction_AccountTransactionsFiat_Endpoint = "account/transactions/fiat";
        protected const string AccountTransaction_AccountTransactionsOrderHistory_Endpoint = "account/transactions/orderHistory";
        protected const string AccountTransaction_AccountTransactionsOrderTrades_Endpoint = "account/transactions/orderTrades";

        // Alarm
        protected const string Alarm_Endpoint = "alarm";
        protected const string Alarm_IdCurrency_Endpoint = "alarm/{id}/{currency}";

        // ApiAccess
        protected const string ApiAccess_Endpoint = "api/access";
        protected const string ApiAccess_Id_Endpoint = "api/access/{id}";

        // Bank
        protected const string Banks_Endpoint = "banks";
        protected const string Banks_Id_Endpoint = "banks/{id}";

        // CryptoDeposit
        protected const string CryptoDeposit_CurrencySymbol_Endpoint = "deposits/crypto/{currencySymbol}";
        protected const string CryptoDeposit_PendingsCurrencySymbol_Endpoint = "deposits/crypto/pendings/{currencySymbol}";

        // CryptoWithdraw
        protected const string Withdrawals_CryptoPendingCurrencySymbol_Endpoint = "withdrawals/crypto/pending/{currencySymbol}";
        protected const string Withdrawals_Crypto_Endpoint = "withdrawals/crypto";
        protected const string Withdrawals_CryptoId_Endpoint = "withdrawals/crypto/{id}";
        protected const string Withdrawals_CryptoCurrencySymbol_Endpoint = "withdrawals/crypto/{currencySymbol}";
        protected const string Withdrawals_CryptValidationCode_Endpoint = "withdrawals/crypto/validation/{code}";
        protected const string Withdrawals_CryptoReceiverAddressesCurrencySymbol_Endpoint = "withdrawals/crypto/receiver-addresses/{currencySymbol}";
        protected const string Withdrawals_CryptoReceiverAddresses_Endpoint = "withdrawals/crypto/receiver-addresses";
        protected const string Withdrawals_CryptoAddressValidation_Endpoint = "withdrawals/crypto/address-validation";
        protected const string Withdrawals_CryptoReceiverAddressesId_Endpoint = "withdrawals/crypto/receiver-addresses/{id}";

        // Device
        protected const string Device_Set_Endpoint = "device/set";
        protected const string Device_Update_Endpoint = "device/update";

        // Exchange
        protected const string Exchange_Commissions_Endpoint = "exchange/commissions";

        // FiatDeposit
        protected const string FiatDeposit_ServiceType_Endpoint = "deposits/fiat/{serviceType}";
        protected const string FiatDeposit_Bank_Endpoint = "deposits/fiat/bank";
        protected const string FiatDeposit_BankId_Endpoint = "deposits/fiat/bank/{id}";
        protected const string FiatDeposit_SendPapara_Endpoint = "deposits/fiat/send-papara";
        protected const string FiatDeposit_Papara_Endpoint = "deposits/fiat/papara";
        protected const string FiatDeposit_PaparaValidationReference_Endpoint = "deposits/fiat/papara/validation/{reference}";

        // FiatWithdrawal
        protected const string FiatWithdrawal_Bank_Endpoint = "withdrawals/fiat/bank";
        protected const string FiatWithdrawal_BankId_Endpoint = "withdrawals/fiat/bank/{id}";
        protected const string FiatWithdrawal_Papara_Endpoint = "withdrawals/fiat/papara";
        protected const string FiatWithdrawal_ConfirmId_Endpoint = "withdrawals/fiat/confirm/{id}";

        // Home
        protected const string Home_ResourcesLanguage_Endpoint = "resources/{language}.json";

        // KnowYourCustomer
        protected const string KnowYourCustomer_Endpoint = "account/kyc";
        protected const string KnowYourCustomer_FileId_Endpoint = "account/kyc/{fileId}";

        // Korder
        protected const string Korder_Endpoint = "korder";

        // OHLC
        protected const string OhlcEndpoint = "ohlc";
        protected const string OhlcVolumesInBtcEndpoint = "ohlc/volumes/btc";

        // Order
        protected const string Order_Endpoint = "order";
        protected const string Order_Id_Endpoint = "order/{id}";
        protected const string Order_OpenOrders_Endpoint = "openOrders";
        protected const string Order_AllOrders_Endpoint = "allOrders";

        // Order Book
        protected const string OrderBook_Endpoint = "orderBook";

        // PinCode
        protected const string PinCode_Endpoint = "account/pincode";
        protected const string PinCode_Status_Endpoint = "account/pincode-status";

        // Price Graph
        protected const string PriceGraph_Config_Endpoint = "pricegraph/config";
        protected const string PriceGraph_SymbolInfo_Endpoint = "pricegraph/symbol_info";
        protected const string PriceGraph_Symbols_Endpoint = "pricegraph/symbols";
        protected const string PriceGraph_Time_Endpoint = "pricegraph/time";
        protected const string PriceGraph_Search_Endpoint = "pricegraph/search";
        protected const string PriceGraph_History_Endpoint = "pricegraph/history";
        protected const string PriceGraph_MobileHistory_Endpoint = "pricegraph/mobile-history";

        // Server
        protected const string Server_Version_Endpoint = "server/version";
        protected const string Server_Time_Endpoint = "server/time";
        protected const string Server_MobileVersion_Endpoint = "server/mobile/version";
        protected const string Server_ExchangeInfo_Endpoint = "server/exchangeInfo";
        protected const string Server_Ping_Endpoint = "server/ping";

        // Ticker
        protected const string Ticker_Endpoint = "ticker";
        protected const string Ticker_Currency_Endpoint = "ticker/currency";

        // Token
        protected const string Token_Endpoint = "token";
        protected const string Token_Mobile_Endpoint = "token/mobile";
        protected const string Token_Refresh_Endpoint = "token/refresh";

        // Trade
        protected const string TradesEndpoint = "trades";

        #endregion

        #region Constructors
        /// <summary>
        /// Create a new instance of BtcTurkClient using the default options
        /// </summary>
        public BtcTurkClient() : this(BtcTurkClientOptions.Default)
        {
        }

        /// <summary>
        /// Create a new instance of the BtcTurkClient with the provided options
        /// </summary>
        public BtcTurkClient(BtcTurkClientOptions options) : base("BtcTurk Rest Api", options)
        {
            Options = options;
            SingleApi = AddApiClient(new BtcTurkClientSingleApi(log, this, options));
        }
        #endregion

        #region Common Methods
        public static void SetDefaultOptions(BtcTurkClientOptions options)
        {
            BtcTurkClientOptions.Default = options;
        }

        public virtual void SetApiCredentials(string apiKey, string apiSecret)
        {
            SingleApi.SetApiCredentials(new ApiCredentials(apiKey, apiSecret));
        }
        #endregion

        #region V1
        public WebCallResult<BtcTurkBalance[]> GetBalances(CancellationToken ct = default) => GetBalancesAsync(ct).Result;
        public async Task<WebCallResult<BtcTurkBalance[]>> GetBalancesAsync(CancellationToken ct = default)
        {
            var result = await SingleApi.ExecuteAsync<BtcTurkApiResponse<BtcTurkBalance[]>>(SingleApi.GetUri(V1_Balances_Endpoint, V1), method: HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
            if (!result.Success) return result.AsError<BtcTurkBalance[]>(new BtcTurkApiError(result.Error.Code, result.Error.Message, result.Error.Data));
            if (result.Data.ErrorCode != "SUCCESS" && result.Data.ErrorCode.ToInt32Safe() > 0) return result.AsError<BtcTurkBalance[]>(new BtcTurkApiError(result.Data.ErrorCode.ToInt32Safe(), result.Data.ErrorMessage, null));

            return result.As(result.Data.Data);
        }

        public WebCallResult<BtcTurkPlacedOrder> PlaceOrder(string pairSymbol, decimal quantity, BtcTurkOrderSide orderSide, BtcTurkOrderMethod orderMethod, decimal? price = null, decimal? stopPrice = null, string clientOrderId = null, CancellationToken ct = default) => PlaceOrderAsync(pairSymbol, quantity, orderSide, orderMethod, price, stopPrice, clientOrderId, ct).Result;
        public async Task<WebCallResult<BtcTurkPlacedOrder>> PlaceOrderAsync(string pairSymbol, decimal quantity, BtcTurkOrderSide orderSide, BtcTurkOrderMethod orderMethod, decimal? price = null, decimal? stopPrice = null, string clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "pairSymbol", pairSymbol },
                { "quantity", quantity.ToString(CultureInfo.InvariantCulture) },
                { "orderType", JsonConvert.SerializeObject(orderSide, new OrderSideConverter(false)) },
                { "orderMethod", JsonConvert.SerializeObject(orderMethod, new OrderMethodConverter(false)) },
            };
            parameters.AddOptionalParameter("price", price?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("stopPrice", stopPrice?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("newClientOrderId", clientOrderId);

            var result = await SingleApi.ExecuteAsync<BtcTurkApiResponse<BtcTurkPlacedOrder>>(SingleApi.GetUri(V1_Order_Endpoint, V1), method: HttpMethod.Post, ct, parameters: parameters, signed: true).ConfigureAwait(false);
            if (!result.Success) return result.AsError<BtcTurkPlacedOrder>(new BtcTurkApiError(result.Error.Code, result.Error.Message, result.Error.Data));
            if (result.Data.ErrorCode != "SUCCESS" && result.Data.ErrorCode.ToInt32Safe() > 0) return result.AsError<BtcTurkPlacedOrder>(new BtcTurkApiError(result.Data.ErrorCode.ToInt32Safe(), result.Data.ErrorMessage, null));

            return result.As(result.Data.Data);
        }

        public WebCallResult<bool> CancelOrder(long orderId, CancellationToken ct = default) => CancelOrderAsync(orderId, ct).Result;
        public async Task<WebCallResult<bool>> CancelOrderAsync(long orderId, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "id", orderId.ToString(CultureInfo.InvariantCulture) },
            };

            var result = await SingleApi.ExecuteAsync<BtcTurkApiResponse<object>>(SingleApi.GetUri(V1_Order_Endpoint, V1), method: HttpMethod.Delete, ct, parameters: parameters, signed: true).ConfigureAwait(false);
            if (!result.Success) return result.AsError<bool>(new BtcTurkApiError(result.Error.Code, result.Error.Message, result.Error.Data));
            if (result.Data.ErrorCode != "SUCCESS" && result.Data.ErrorCode.ToInt32Safe() > 0) return result.AsError<bool>(new BtcTurkApiError(result.Data.ErrorCode.ToInt32Safe(), result.Data.ErrorMessage, null));

            return result.As(result.Data.Success);
        }

        public WebCallResult<BtcTurkOrder[]> GetAllOrders(string pairSymbol, long? startOrderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = 100, int? page = 1, CancellationToken ct = default) => GetAllOrdersAsync(pairSymbol, startOrderId, startTime, endTime, limit, page, ct).Result;
        public async Task<WebCallResult<BtcTurkOrder[]>> GetAllOrdersAsync(string pairSymbol, long? startOrderId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = 100, int? page = 1, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "pairSymbol", pairSymbol },
            };
            parameters.AddOptionalParameter("orderId", startOrderId != null ? startOrderId : null);
            parameters.AddOptionalParameter("startTime", startTime != null ? ToUnixTimestamp(startTime.Value).ToString() : null);
            parameters.AddOptionalParameter("endTime", endTime != null ? ToUnixTimestamp(endTime.Value).ToString() : null);
            parameters.AddOptionalParameter("limit", limit != null ? limit : null);
            parameters.AddOptionalParameter("page", page != null ? page - 1 : null);

            var result = await SingleApi.ExecuteAsync<BtcTurkApiResponse<BtcTurkOrder[]>>(SingleApi.GetUri(V1_AllOrders_Endpoint, V1), method: HttpMethod.Get, ct, parameters: parameters, signed: true).ConfigureAwait(false);
            if (!result.Success) return result.AsError<BtcTurkOrder[]>(new BtcTurkApiError(result.Error.Code, result.Error.Message, result.Error.Data));
            if (result.Data.ErrorCode != "SUCCESS" && result.Data.ErrorCode.ToInt32Safe() > 0) return result.AsError<BtcTurkOrder[]>(new BtcTurkApiError(result.Data.ErrorCode.ToInt32Safe(), result.Data.ErrorMessage, null));

            return result.As(result.Data.Data);
        }

        public WebCallResult<BtcTurkOpenOrders> GetOpenOrders(string pairSymbol = null, CancellationToken ct = default) => GetOpenOrdersAsync(pairSymbol, ct).Result;
        public async Task<WebCallResult<BtcTurkOpenOrders>> GetOpenOrdersAsync(string pairSymbol = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("pairSymbol", pairSymbol != null ? pairSymbol : null);

            var result = await SingleApi.ExecuteAsync<BtcTurkApiResponse<BtcTurkOpenOrders>>(SingleApi.GetUri(V1_OpenOrders_Endpoint, V1), method: HttpMethod.Get, ct, parameters: parameters, signed: true).ConfigureAwait(false);
            if (!result.Success) return result.AsError<BtcTurkOpenOrders>(new BtcTurkApiError(result.Error.Code, result.Error.Message, result.Error.Data));
            if (result.Data.ErrorCode != "SUCCESS" && result.Data.ErrorCode.ToInt32Safe() > 0) return result.AsError<BtcTurkOpenOrders>(new BtcTurkApiError(result.Data.ErrorCode.ToInt32Safe(), result.Data.ErrorMessage, null));

            return result.As(result.Data.Data);
        }

        public WebCallResult<BtcTurkTrade[]> GetTradesV1(string pairSymbol, int last = 50, CancellationToken ct = default) => GetTradesV1Async(pairSymbol, last, ct).Result;
        public async Task<WebCallResult<BtcTurkTrade[]>> GetTradesV1Async(string pairSymbol, int last = 50, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "pairSymbol", pairSymbol },
                { "last", last.ToString() },
            };

            var result = await SingleApi.ExecuteAsync<BtcTurkApiResponse<BtcTurkTrade[]>>(SingleApi.GetUri(V1_Trades_Endpoint, V1), method: HttpMethod.Get, ct, parameters: parameters).ConfigureAwait(false);
            if (!result.Success) return result.AsError<BtcTurkTrade[]>(new BtcTurkApiError(result.Error.Code, result.Error.Message, result.Error.Data));
            if (result.Data.ErrorCode != "SUCCESS" && result.Data.ErrorCode.ToInt32Safe() > 0) return result.AsError<BtcTurkTrade[]>(new BtcTurkApiError(result.Data.ErrorCode.ToInt32Safe(), result.Data.ErrorMessage, null));

            return result.As(result.Data.Data);
        }

        public WebCallResult<BtcTurkTransaction[]> GetTradeTransactions(string[] symbol = null, BtcTurkOrderSide[] type = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default) => GetTradeTransactionsAsync(symbol, type, startTime, endTime, ct).Result;
        public async Task<WebCallResult<BtcTurkTransaction[]>> GetTradeTransactionsAsync(string[] symbol = null, BtcTurkOrderSide[] type = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("type", JsonConvert.DeserializeObject<string[]>(JsonConvert.SerializeObject(type, new OrderSideConverter(true))));
            parameters.AddOptionalParameter("startTime", startTime != null ? ToUnixTimestamp(startTime.Value).ToString() : null);
            parameters.AddOptionalParameter("endTime", endTime != null ? ToUnixTimestamp(endTime.Value).ToString() : null);

            var result = await SingleApi.ExecuteAsync<BtcTurkApiResponse<BtcTurkTransaction[]>>(SingleApi.GetUri(V1_TransactionsTrade_Endpoint, V1), method: HttpMethod.Get, ct, parameters: parameters, signed: true).ConfigureAwait(false);
            if (!result.Success) return result.AsError<BtcTurkTransaction[]>(new BtcTurkApiError(result.Error.Code, result.Error.Message, result.Error.Data));
            if (result.Data.ErrorCode != "SUCCESS" && result.Data.ErrorCode.ToInt32Safe() > 0) return result.AsError<BtcTurkTransaction[]>(new BtcTurkApiError(result.Data.ErrorCode.ToInt32Safe(), result.Data.ErrorMessage, null));

            return result.As(result.Data.Data);
        }

        public WebCallResult<BtcTurkPairTransaction[]> GetTradeTransactionsByPair(string pairSymbol, CancellationToken ct = default) => GetTradeTransactionsByPairAsync(pairSymbol, ct).Result;
        public async Task<WebCallResult<BtcTurkPairTransaction[]>> GetTradeTransactionsByPairAsync(string pairSymbol, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "pairSymbol", pairSymbol },
            };

            var result = await SingleApi.ExecuteAsync<BtcTurkApiResponse<BtcTurkPairTransaction[]>>(SingleApi.GetUri(V1_TransactionsTradeByPair_Endpoint.Replace("{pairSymbol}", pairSymbol), V1), method: HttpMethod.Get, ct, parameters: parameters, signed: true).ConfigureAwait(false);
            if (!result.Success) return result.AsError<BtcTurkPairTransaction[]>(new BtcTurkApiError(result.Error.Code, result.Error.Message, result.Error.Data));
            if (result.Data.ErrorCode != "SUCCESS" && result.Data.ErrorCode.ToInt32Safe() > 0) return result.AsError<BtcTurkPairTransaction[]>(new BtcTurkApiError(result.Data.ErrorCode.ToInt32Safe(), result.Data.ErrorMessage, null));

            return result.As(result.Data.Data);
        }

        public WebCallResult<BtcTurkCryptoFiatTransaction[]> GetCryptoTransactions(string[] symbol = null, BtcTurkOrderSide[] type = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default) => GetCryptoTransactionsAsync(symbol, type, startTime, endTime, ct).Result;
        public async Task<WebCallResult<BtcTurkCryptoFiatTransaction[]>> GetCryptoTransactionsAsync(string[] symbol = null, BtcTurkOrderSide[] type = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("type", JsonConvert.DeserializeObject<string[]>(JsonConvert.SerializeObject(type, new OrderSideConverter(true))));
            parameters.AddOptionalParameter("startTime", startTime != null ? ToUnixTimestamp(startTime.Value).ToString() : null);
            parameters.AddOptionalParameter("endTime", endTime != null ? ToUnixTimestamp(endTime.Value).ToString() : null);

            var result = await SingleApi.ExecuteAsync<BtcTurkApiResponse<BtcTurkCryptoFiatTransaction[]>>(SingleApi.GetUri(V1_TransactionsCrypto_Endpoint, V1), method: HttpMethod.Get, ct, parameters: parameters).ConfigureAwait(false);
            if (!result.Success) return result.AsError<BtcTurkCryptoFiatTransaction[]>(new BtcTurkApiError(result.Error.Code, result.Error.Message, result.Error.Data));
            if (result.Data.ErrorCode != "SUCCESS" && result.Data.ErrorCode.ToInt32Safe() > 0) return result.AsError<BtcTurkCryptoFiatTransaction[]>(new BtcTurkApiError(result.Data.ErrorCode.ToInt32Safe(), result.Data.ErrorMessage, null));

            return result.As(result.Data.Data);
        }

        public WebCallResult<BtcTurkCryptoFiatTransaction[]> GetFiatTransactions(string[] symbol = null, BtcTurkOrderSide[] type = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default) => GetFiatTransactionsAsync(symbol, type, startTime, endTime, ct).Result;
        public async Task<WebCallResult<BtcTurkCryptoFiatTransaction[]>> GetFiatTransactionsAsync(string[] symbol = null, BtcTurkOrderSide[] type = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("type", JsonConvert.DeserializeObject<string[]>(JsonConvert.SerializeObject(type, new OrderSideConverter(true))));
            parameters.AddOptionalParameter("startTime", startTime != null ? ToUnixTimestamp(startTime.Value).ToString() : null);
            parameters.AddOptionalParameter("endTime", endTime != null ? ToUnixTimestamp(endTime.Value).ToString() : null);

            var result = await SingleApi.ExecuteAsync<BtcTurkApiResponse<BtcTurkCryptoFiatTransaction[]>>(SingleApi.GetUri(V1_TransactionsFiat_Endpoint, V1), method: HttpMethod.Get, ct, parameters: parameters, signed: true).ConfigureAwait(false);
            if (!result.Success) return result.AsError<BtcTurkCryptoFiatTransaction[]>(new BtcTurkApiError(result.Error.Code, result.Error.Message, result.Error.Data));
            if (result.Data.ErrorCode != "SUCCESS" && result.Data.ErrorCode.ToInt32Safe() > 0) return result.AsError<BtcTurkCryptoFiatTransaction[]>(new BtcTurkApiError(result.Data.ErrorCode.ToInt32Safe(), result.Data.ErrorMessage, null));

            return result.As(result.Data.Data);
        }

        #endregion

        #region Exchange
        public WebCallResult<BtcTurkCommission> GetExchangeCommissions(CancellationToken ct = default) => GetExchangeCommissionsAsync(ct).Result;
        public async Task<WebCallResult<BtcTurkCommission>> GetExchangeCommissionsAsync(CancellationToken ct = default)
        {
            var result = await SingleApi.ExecuteAsync<BtcTurkApiResponse<BtcTurkCommission>>(SingleApi.GetUri(Exchange_Commissions_Endpoint, PublicVersion), method: HttpMethod.Get, ct).ConfigureAwait(false);
            if (!result.Success) return result.AsError<BtcTurkCommission>(new BtcTurkApiError(result.Error.Code, result.Error.Message, result.Error.Data));
            if (result.Data.ErrorCode != "SUCCESS" && result.Data.ErrorCode.ToInt32Safe() > 0) return result.AsError<BtcTurkCommission>(new BtcTurkApiError(result.Data.ErrorCode.ToInt32Safe(), result.Data.ErrorMessage, null));

            return result.As(result.Data.Data);
        }
        #endregion

        #region Home
        public WebCallResult<BtcTurkResources> GetResources(string language = "tr-TR", CancellationToken ct = default) => GetResourcesAsync(language, ct).Result;
        public async Task<WebCallResult<BtcTurkResources>> GetResourcesAsync(string language = "tr-TR", CancellationToken ct = default)
        {
            var result = await SingleApi.ExecuteAsync<BtcTurkResources>(SingleApi.GetUri(Home_ResourcesLanguage_Endpoint.Replace("{language}", language), NoVersion), method: HttpMethod.Get, ct).ConfigureAwait(false);
            if (!result.Success) return result.AsError<BtcTurkResources>(new BtcTurkApiError(result.Error.Code, result.Error.Message, result.Error.Data));

            return result.As(result.Data);
        }
        #endregion

        #region OHLC
        public WebCallResult<BtcTurkOhlc[]> GetOhlc(string pairSymbol, int last = 50, CancellationToken ct = default) => GetOhlcAsync(pairSymbol, last, ct).Result;
        public async Task<WebCallResult<BtcTurkOhlc[]>> GetOhlcAsync(string pairSymbol, int last = 50, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "pairSymbol", pairSymbol },
            };
            parameters.AddOptionalParameter("last", last);

            var result = await SingleApi.ExecuteAsync<BtcTurkApiResponse<BtcTurkOhlc[]>>(SingleApi.GetUri(OhlcEndpoint, PublicVersion), method: HttpMethod.Get, ct, parameters: parameters).ConfigureAwait(false);
            if (!result.Success) return result.AsError<BtcTurkOhlc[]>(new BtcTurkApiError(result.Error.Code, result.Error.Message, result.Error.Data));
            if (result.Data.ErrorCode != "SUCCESS" && result.Data.ErrorCode.ToInt32Safe() > 0) return result.AsError<BtcTurkOhlc[]>(new BtcTurkApiError(result.Data.ErrorCode.ToInt32Safe(), result.Data.ErrorMessage, null));

            return result.As(result.Data.Data);
        }

        public WebCallResult<BtcTurkOhlcVolumes[]> GetOhlcVolumesInBtc(CancellationToken ct = default) => GetOhlcVolumesInBtcAsync(ct).Result;
        public async Task<WebCallResult<BtcTurkOhlcVolumes[]>> GetOhlcVolumesInBtcAsync(CancellationToken ct = default)
        {
            var result = await SingleApi.ExecuteAsync<BtcTurkApiResponse<BtcTurkOhlcVolumes[]>>(SingleApi.GetUri(OhlcVolumesInBtcEndpoint, PublicVersion), method: HttpMethod.Get, ct).ConfigureAwait(false);
            if (!result.Success) return result.AsError<BtcTurkOhlcVolumes[]>(new BtcTurkApiError(result.Error.Code, result.Error.Message, result.Error.Data));
            if (result.Data.ErrorCode != "SUCCESS" && result.Data.ErrorCode.ToInt32Safe() > 0) return result.AsError<BtcTurkOhlcVolumes[]>(new BtcTurkApiError(result.Data.ErrorCode.ToInt32Safe(), result.Data.ErrorMessage, null));

            return result.As(result.Data.Data);
        }
        #endregion

        #region Order Book
        public WebCallResult<BtcTurkOrderBook> GetOrderBook(string pairSymbol, int limit = 100, CancellationToken ct = default) => GetOrderBookAsync(pairSymbol, limit, ct).Result;
        public async Task<WebCallResult<BtcTurkOrderBook>> GetOrderBookAsync(string pairSymbol, int limit = 100, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "pairSymbol", pairSymbol },
                { "limit", limit },
            };

            var result = await SingleApi.ExecuteAsync<BtcTurkApiResponse<BtcTurkOrderBook>>(SingleApi.GetUri(OrderBook_Endpoint, PublicVersion), method: HttpMethod.Get, ct, parameters: parameters).ConfigureAwait(false);
            if (!result.Success) return result.AsError<BtcTurkOrderBook>(new BtcTurkApiError(result.Error.Code, result.Error.Message, result.Error.Data));
            if (result.Data.ErrorCode != "SUCCESS" && result.Data.ErrorCode.ToInt32Safe() > 0) return result.AsError<BtcTurkOrderBook>(new BtcTurkApiError(result.Data.ErrorCode.ToInt32Safe(), result.Data.ErrorMessage, null));

            return result.As(result.Data.Data);
        }
        #endregion

        #region Price Graph
        public WebCallResult<BtcTurkPriceGraphConfig> GetPriceGraphConfig(CancellationToken ct = default) => GetPriceGraphConfigAsync(ct).Result;
        public async Task<WebCallResult<BtcTurkPriceGraphConfig>> GetPriceGraphConfigAsync(CancellationToken ct = default)
        {
            var result = await SingleApi.ExecuteAsync<BtcTurkPriceGraphConfig>(SingleApi.GetUri(PriceGraph_Config_Endpoint, PublicVersion), method: HttpMethod.Get, ct).ConfigureAwait(false);
            if (!result.Success) return result.AsError<BtcTurkPriceGraphConfig>(new BtcTurkApiError(result.Error.Code, result.Error.Message, result.Error.Data));

            return result.As(result.Data);
        }

        public WebCallResult<BtcTurkPriceGraphSymbolInfo> GetPriceGraphSymbolInfo(string group = "", CancellationToken ct = default) => GetPriceGraphSymbolInfoAsync(group, ct).Result;
        public async Task<WebCallResult<BtcTurkPriceGraphSymbolInfo>> GetPriceGraphSymbolInfoAsync(string group = "", CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "group", group },
            };

            var result = await SingleApi.ExecuteAsync<BtcTurkPriceGraphSymbolInfo>(SingleApi.GetUri(PriceGraph_SymbolInfo_Endpoint, PublicVersion), method: HttpMethod.Get, ct, parameters: parameters).ConfigureAwait(false);
            if (!result.Success) return result.AsError<BtcTurkPriceGraphSymbolInfo>(new BtcTurkApiError(result.Error.Code, result.Error.Message, result.Error.Data));

            return result.As(result.Data);
        }

        public WebCallResult<BtcTurkPriceGraphSymbols> GetPriceGraphSymbols(string symbol, CancellationToken ct = default) => GetPriceGraphSymbolsAsync(symbol, ct).Result;
        public async Task<WebCallResult<BtcTurkPriceGraphSymbols>> GetPriceGraphSymbolsAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
            };

            var result = await SingleApi.ExecuteAsync<BtcTurkPriceGraphSymbols>(SingleApi.GetUri(PriceGraph_Symbols_Endpoint, PublicVersion), method: HttpMethod.Get, ct, parameters: parameters).ConfigureAwait(false);
            if (!result.Success) return result.AsError<BtcTurkPriceGraphSymbols>(new BtcTurkApiError(result.Error.Code, result.Error.Message, result.Error.Data));

            return result.As(result.Data);
        }

        public WebCallResult<BtcTurkKline[]> GetPriceGraphHistory(string symbol, BtcTurkPeriod period, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default) => GetPriceGraphHistoryAsync(symbol, period, startTime, endTime, ct).Result;
        public async Task<WebCallResult<BtcTurkKline[]>> GetPriceGraphHistoryAsync(string symbol, BtcTurkPeriod period, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        => await GetPriceGraphHistoryCoreAsync(PriceGraph_History_Endpoint, symbol, period, startTime, endTime);

        public WebCallResult<BtcTurkKline[]> GetPriceGraphMobileHistory(string symbol, BtcTurkPeriod period, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default) => GetPriceGraphMobileHistoryAsync(symbol, period, startTime, endTime, ct).Result;
        public async Task<WebCallResult<BtcTurkKline[]>> GetPriceGraphMobileHistoryAsync(string symbol, BtcTurkPeriod period, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        => await GetPriceGraphHistoryCoreAsync(PriceGraph_MobileHistory_Endpoint, symbol, period, startTime, endTime, ct);

        protected virtual async Task<WebCallResult<BtcTurkKline[]>> GetPriceGraphHistoryCoreAsync(string endpoint, string symbol, BtcTurkPeriod period, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object> {
                { "symbol", symbol },
                { "resolution", JsonConvert.SerializeObject(period, new PeriodEnumConverter(false)) }
            };
            parameters.AddOptionalParameter("from", startTime != null ? startTime.Value.ToUnixTimeSeconds().ToString() : "1");
            parameters.AddOptionalParameter("to", endTime != null ? endTime.Value.ToUnixTimeSeconds().ToString() : DateTime.UtcNow.ToUnixTimeSeconds().ToString());

            var result = await SingleApi.ExecuteAsync<BtcTurkKlineData>(SingleApi.GetUri(endpoint, PublicVersion), method: HttpMethod.Get, ct, parameters: parameters).ConfigureAwait(false);
            if (!result.Success || result.Data == null
                || result.Data.Times == null
                || result.Data.Opens == null
                || result.Data.Highs == null
                || result.Data.Lows == null
                || result.Data.Closes == null
                || result.Data.Volumes == null)
                return result.AsError<BtcTurkKline[]>(new BtcTurkApiError(result.Error.Code, result.Error.Message, result.Error.Data));

            // Parse Result
            List<BtcTurkKline> response = new List<BtcTurkKline>();
            var maxRows = Math.Min(result.Data.Times.Length, result.Data.Opens.Length);
            maxRows = Math.Min(maxRows, result.Data.Highs.Length);
            maxRows = Math.Min(maxRows, result.Data.Lows.Length);
            maxRows = Math.Min(maxRows, result.Data.Closes.Length);
            maxRows = Math.Min(maxRows, result.Data.Volumes.Length);

            for (int i = 0; i < maxRows; i++)
            {
                response.Add(new BtcTurkKline
                {
                    OpenDateTime = result.Data.Times[i].FromUnixTimeSeconds(),
                    Open = result.Data.Opens[i],
                    High = result.Data.Highs[i],
                    Low = result.Data.Lows[i],
                    Close = result.Data.Closes[i],
                    Volume = result.Data.Volumes[i],
                });
            }

            return result.As(response.ToArray());
        }
        #endregion

        #region Server
        public WebCallResult<BtcTurkServerVersion> GetServerVersion(CancellationToken ct = default) => GetServerVersionAsync(ct).Result;
        public async Task<WebCallResult<BtcTurkServerVersion>> GetServerVersionAsync(CancellationToken ct = default)
        {
            var result = await SingleApi.ExecuteAsync<BtcTurkServerVersion>(SingleApi.GetUri(Server_Version_Endpoint, PublicVersion), method: HttpMethod.Get, ct).ConfigureAwait(false);
            if (!result.Success) return result.AsError<BtcTurkServerVersion>(new BtcTurkApiError(result.Error.Code, result.Error.Message, result.Error.Data));

            return result.As(result.Data);
        }

        public WebCallResult<BtcTurkTime> GetServerTime(CancellationToken ct = default) => GetServerTimeAsync(ct).Result;
        public async Task<WebCallResult<BtcTurkTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var result = await SingleApi.ExecuteAsync<BtcTurkTime>(SingleApi.GetUri(Server_Time_Endpoint, PublicVersion), method: HttpMethod.Get, ct).ConfigureAwait(false);
            if (!result.Success) return result.AsError<BtcTurkTime>(new BtcTurkApiError(result.Error.Code, result.Error.Message, result.Error.Data));

            return result.As(result.Data);
        }

        internal WebCallResult<DateTime> GetTime(CancellationToken ct = default) => GetTimeAsync(ct).Result;
        internal async Task<WebCallResult<DateTime>> GetTimeAsync(CancellationToken ct = default)
        {
            var result = await SingleApi.ExecuteAsync<BtcTurkTime>(SingleApi.GetUri(Server_Time_Endpoint, PublicVersion), method: HttpMethod.Get, ct).ConfigureAwait(false);
            if (!result.Success) return result.AsError<DateTime>(new BtcTurkApiError(result.Error.Code, result.Error.Message, result.Error.Data));

            return result.As(result.Data.ServerTime);
        }

        public WebCallResult<int> GetServerMobileVersion(string os, string version, CancellationToken ct = default) => GetServerMobileVersionAsync(os, version, ct).Result;
        public async Task<WebCallResult<int>> GetServerMobileVersionAsync(string os, string version, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "os", os },
                { "ver", version },
            };

            var result = await SingleApi.ExecuteAsync<BtcTurkApiResponse<int>>(SingleApi.GetUri(Server_MobileVersion_Endpoint, PublicVersion), method: HttpMethod.Get, ct, parameters: parameters).ConfigureAwait(false);
            if (!result.Success) return result.AsError<int>(new BtcTurkApiError(result.Error.Code, result.Error.Message, result.Error.Data));
            if (result.Data.ErrorCode != "SUCCESS" && result.Data.ErrorCode.ToInt32Safe() > 0) return result.AsError<int>(new BtcTurkApiError(result.Data.ErrorCode.ToInt32Safe(), result.Data.ErrorMessage, null));

            return result.As(result.Data.Data);
        }

        public WebCallResult<BtcTurkServerExchangeInfo> GetServerExchangeInfo(CancellationToken ct = default) => GetServerExchangeInfoAsync(ct).Result;
        public async Task<WebCallResult<BtcTurkServerExchangeInfo>> GetServerExchangeInfoAsync(CancellationToken ct = default)
        {
            var result = await SingleApi.ExecuteAsync<BtcTurkApiResponse<BtcTurkServerExchangeInfo>>(SingleApi.GetUri(Server_ExchangeInfo_Endpoint, PublicVersion), method: HttpMethod.Get, ct).ConfigureAwait(false);
            if (!result.Success) return result.AsError<BtcTurkServerExchangeInfo>(new BtcTurkApiError(result.Error.Code, result.Error.Message, result.Error.Data));
            if (result.Data.ErrorCode != "SUCCESS" && result.Data.ErrorCode.ToInt32Safe() > 0) return result.AsError<BtcTurkServerExchangeInfo>(new BtcTurkApiError(result.Data.ErrorCode.ToInt32Safe(), result.Data.ErrorMessage, null));

            return result.As(result.Data.Data);
        }

        public WebCallResult<BtcTurkServerPing> GetServerPing(CancellationToken ct = default) => GetServerPingAsync(ct).Result;
        public async Task<WebCallResult<BtcTurkServerPing>> GetServerPingAsync(CancellationToken ct = default)
        {
            var result = await SingleApi.ExecuteAsync<BtcTurkServerPing>(SingleApi.GetUri(Server_Ping_Endpoint, PublicVersion), method: HttpMethod.Get, ct).ConfigureAwait(false);
            if (!result.Success) return result.AsError<BtcTurkServerPing>(new BtcTurkApiError(result.Error.Code, result.Error.Message, result.Error.Data));

            return result.As(result.Data);
        }
        #endregion

        #region Ticker
        public WebCallResult<BtcTurkTicker[]> GetTicker(string pairSymbol = "", CancellationToken ct = default) => GetTickerAsync(pairSymbol, ct).Result;
        public async Task<WebCallResult<BtcTurkTicker[]>> GetTickerAsync(string pairSymbol = "", CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "pairSymbol", pairSymbol },
            };

            var result = await SingleApi.ExecuteAsync<BtcTurkApiResponse<BtcTurkTicker[]>>(SingleApi.GetUri(Ticker_Endpoint, PublicVersion), method: HttpMethod.Get, ct, parameters: parameters).ConfigureAwait(false);
            if (!result.Success) return result.AsError<BtcTurkTicker[]>(new BtcTurkApiError(result.Error.Code, result.Error.Message, result.Error.Data));
            if (result.Data.ErrorCode != "SUCCESS" && result.Data.ErrorCode.ToInt32Safe() > 0) return result.AsError<BtcTurkTicker[]>(new BtcTurkApiError(result.Data.ErrorCode.ToInt32Safe(), result.Data.ErrorMessage, null));

            return result.As(result.Data.Data);
        }

        public WebCallResult<BtcTurkTicker[]> GetTickerByCurrency(string currencySymbol, CancellationToken ct = default) => GetTickerByCurrencyAsync(currencySymbol, ct).Result;
        public async Task<WebCallResult<BtcTurkTicker[]>> GetTickerByCurrencyAsync(string currencySymbol, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "symbol", currencySymbol },
            };

            var result = await SingleApi.ExecuteAsync<BtcTurkApiResponse<BtcTurkTicker[]>>(SingleApi.GetUri(Ticker_Currency_Endpoint, PublicVersion), method: HttpMethod.Get, ct, parameters: parameters).ConfigureAwait(false);
            if (!result.Success) return result.AsError<BtcTurkTicker[]>(new BtcTurkApiError(result.Error.Code, result.Error.Message, result.Error.Data));
            if (result.Data.ErrorCode != "SUCCESS" && result.Data.ErrorCode.ToInt32Safe() > 0) return result.AsError<BtcTurkTicker[]>(new BtcTurkApiError(result.Data.ErrorCode.ToInt32Safe(), result.Data.ErrorMessage, null));

            return result.As(result.Data.Data);
        }
        #endregion

        #region Trade
        public WebCallResult<BtcTurkTrade[]> GetTradesV2(string pairSymbol/*, int last=50*/, CancellationToken ct = default) => GetTradesV2Async(pairSymbol/*, last*/, ct).Result;
        public async Task<WebCallResult<BtcTurkTrade[]>> GetTradesV2Async(string pairSymbol/*, int last = 50*/, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, object>
            {
                { "pairSymbol", pairSymbol },
                /*{ "last", last.ToString() },*/
            };

            var result = await SingleApi.ExecuteAsync<BtcTurkApiResponse<BtcTurkTrade[]>>(SingleApi.GetUri(TradesEndpoint, PublicVersion), method: HttpMethod.Get, ct, parameters: parameters).ConfigureAwait(false);
            if (!result.Success) return result.AsError<BtcTurkTrade[]>(new BtcTurkApiError(result.Error.Code, result.Error.Message, result.Error.Data));
            if (result.Data.ErrorCode != "SUCCESS" && result.Data.ErrorCode.ToInt32Safe() > 0) return result.AsError<BtcTurkTrade[]>(new BtcTurkApiError(result.Data.ErrorCode.ToInt32Safe(), result.Data.ErrorMessage, null));

            return result.As(result.Data.Data);
        }
        #endregion

        #region Protected Methods
        protected override Error ParseErrorResponse(JToken error)
        {
            return this.BtcTurkParseErrorResponse(error);
        }
        protected virtual Error BtcTurkParseErrorResponse(JToken error)
        {
            if (error["code"] == null || error["message"] == null)
                return new ServerError(error.ToString());

            return new ServerError($"{(string)error["code"]}, {(string)error["message"]}");
        }

        protected static long ToUnixTimestamp(DateTime time)
        {
            return (long)(time - new DateTime(1970, 1, 1)).TotalMilliseconds;
        }

        internal async Task<WebCallResult> ExecuteAsync(RestApiClient apiClient, Uri uri, HttpMethod method, CancellationToken ct, Dictionary<string, object> parameters = null, bool signed = false, HttpMethodParameterPosition? parameterPosition = null)
        {
            var result = await SendRequestAsync<object>(apiClient, uri, method, ct, parameters, signed, parameterPosition).ConfigureAwait(false);
            if (!result) return result.AsDatalessError(result.Error!);

            return result.AsDataless();
        }

        internal async Task<WebCallResult<T>> ExecuteAsync<T>(RestApiClient apiClient, Uri uri, HttpMethod method, CancellationToken ct, Dictionary<string, object> parameters = null, bool signed = false, int weight = 1, bool ignoreRatelimit = false, HttpMethodParameterPosition? parameterPosition = null) where T : class
        {
            var result = await SendRequestAsync<T>(apiClient, uri, method, ct, parameters, signed, parameterPosition, requestWeight: weight, ignoreRatelimit: ignoreRatelimit).ConfigureAwait(false);
            if (!result) return result.AsError<T>(result.Error!);

            return result.As(result.Data);
        }
        #endregion

    }

    public class BtcTurkClientSingleApi : RestApiClient
    {
        #region Internal Fields
        internal readonly Log _log;
        internal readonly BtcTurkClient _baseClient;
        internal readonly BtcTurkClientOptions _options;
        internal static TimeSyncState TimeSyncState = new TimeSyncState("BtcTurk One Api");
        #endregion

        internal BtcTurkClientSingleApi(Log log, BtcTurkClient baseClient, BtcTurkClientOptions options) : base(options, options.SingleApiOptions)
        {
            _baseClient = baseClient;
            _options = options;
            _log = log;
        }

        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new BtcTurkAuthenticationProvider(credentials);

        internal Task<WebCallResult> ExecuteAsync(Uri uri, HttpMethod method, CancellationToken ct, Dictionary<string, object> parameters = null, bool signed = false, HttpMethodParameterPosition? parameterPosition = null)
         => _baseClient.ExecuteAsync(this, uri, method, ct, parameters, signed, parameterPosition: parameterPosition);

        internal Task<WebCallResult<T>> ExecuteAsync<T>(Uri uri, HttpMethod method, CancellationToken ct, Dictionary<string, object> parameters = null, bool signed = false, int weight = 1, bool ignoreRatelimit = false, HttpMethodParameterPosition? parameterPosition = null) where T : class
         => _baseClient.ExecuteAsync<T>(this, uri, method, ct, parameters, signed, weight, ignoreRatelimit: ignoreRatelimit, parameterPosition: parameterPosition);

        internal Uri GetUri(string endpoint, string version = null)
        {
            return string.IsNullOrEmpty(version) ? new Uri($"{BaseAddress.TrimEnd('/')}/{endpoint}") : new Uri($"{BaseAddress.TrimEnd('/')}/v{version}/{endpoint}");
        }

        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
             => _baseClient.GetTimeAsync();

        public override TimeSyncInfo GetTimeSyncInfo()
            => new TimeSyncInfo(_log, _options.SingleApiOptions.AutoTimestamp, _options.SingleApiOptions.TimestampRecalculationInterval, TimeSyncState);

        public override TimeSpan GetTimeOffset()
            => TimeSyncState.TimeOffset;
    }

}