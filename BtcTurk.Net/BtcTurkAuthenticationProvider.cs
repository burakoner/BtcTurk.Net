using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace BtcTurk.Net
{
    public class BtcTurkAuthenticationProvider : AuthenticationProvider
    {
        private readonly HMACSHA256 encryptor;
        private readonly ArrayParametersSerialization arraySerialization;

        public BtcTurkAuthenticationProvider(ApiCredentials credentials, ArrayParametersSerialization arraySerialization) : base(credentials)
        {
            encryptor = new HMACSHA256(Encoding.ASCII.GetBytes(credentials.Secret.GetString()));
            this.arraySerialization = arraySerialization;
        }
        /*
        public override Dictionary<string, object> AddAuthenticationToParameters(string uri, string method, Dictionary<string, object> parameters, bool signed)
        {
            if (!signed)
                return parameters;

            /*
            var query = parameters.CreateParamString(true, arraySerialization);
            parameters.Add("signature", ByteToString(encryptor.ComputeHash(Encoding.UTF8.GetBytes(query))));
            * /

            // Dummy Return
            return parameters;
        }

        public override Dictionary<string, string> AddAuthenticationToHeaders(string uri, string method, Dictionary<string, object> parameters, bool signed)
        {
            if (uri.Contains("/v1/"))
            {
                var sign = string.Empty;
                var apiKey = Credentials.Key.GetString();
                var unixTime = DateTime.UtcNow.ToUnixTimeMilliSeconds();

                using (HMACSHA256 hmac = new HMACSHA256(Convert.FromBase64String(Credentials.Secret.GetString())))
                {
                    byte[] signatureBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(apiKey + unixTime));
                    sign = Convert.ToBase64String(signatureBytes);
                }

                return new Dictionary<string, string> {
                { "X-PCK", Credentials.Key.GetString() },
                { "X-Stamp", unixTime.ToString() },
                { "X-Signature", sign },
            };
            }
            else if (uri.Contains("/v1/"))
            {
                return new Dictionary<string, string>();
            }

            // Dummy Return
            return new Dictionary<string, string>();
        }
        */
        public override string Sign(string toSign)
        {
            throw new System.NotImplementedException();
        }

    }
}
