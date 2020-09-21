using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Net.Http;
using System.Web;

namespace BtcTurk.Net
{
    public class BtcTurkAuthenticationProvider : AuthenticationProvider
    {
        private readonly HMACSHA256 encryptor;
        private readonly ArrayParametersSerialization arraySerialization;

        public BtcTurkAuthenticationProvider(ApiCredentials credentials, ArrayParametersSerialization arraySerialization) : base(credentials)
        {
            if (credentials.Secret == null)
                throw new ArgumentException("No valid API credentials provided. Key/Secret needed.");

            encryptor = new HMACSHA256(Encoding.ASCII.GetBytes(credentials.Secret.GetString()));
            this.arraySerialization = arraySerialization;
        }

        public override Dictionary<string, string> AddAuthenticationToHeaders(string uri, HttpMethod method, Dictionary<string, object> parameters, bool signed, PostParameters postParameterPosition, ArrayParametersSerialization arraySerialization)
        {
            if (Credentials.Key == null || Credentials.Secret == null)
                throw new ArgumentException("No valid API credentials provided. Key/Secret needed.");

            if (uri.Contains("/v1/"))
            {
                var sign = string.Empty;
                var apiKey = Credentials.Key.GetString();
                var apiSecret = Credentials.Secret.GetString();
                var nonce = DateTime.UtcNow.ToUnixTimeMilliseconds();
                string message = apiKey + nonce;
                using (HMACSHA256 hmac = new HMACSHA256(Convert.FromBase64String(apiSecret)))
                {
                    byte[] signatureBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
                    sign = Convert.ToBase64String(signatureBytes);
                }

                return new Dictionary<string, string> {
                { "X-PCK", Credentials.Key.GetString() },
                { "X-Stamp", nonce.ToString() },
                { "X-Signature", sign },
            };
            }
            else if (uri.Contains("/v3/"))
            {
                return new Dictionary<string, string>();
            }

            return new Dictionary<string, string>();
        }

        public override string Sign(string toSign)
        {
            throw new NotImplementedException();
        }
    }
}