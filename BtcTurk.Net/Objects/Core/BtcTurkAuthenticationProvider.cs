namespace BtcTurk.Net.Objects.Core;

public class BtcTurkAuthenticationProvider : AuthenticationProvider
{
    private readonly HMACSHA256 encryptor;

    public BtcTurkAuthenticationProvider(ApiCredentials credentials) : base(credentials)
    {
        if (credentials == null || credentials.Secret == null)
            throw new ArgumentException("No valid API credentials provided. Key/Secret needed.");

        encryptor = new HMACSHA256(Encoding.ASCII.GetBytes(credentials.Secret.GetString()));
    }

    public override void AuthenticateRequest(RestApiClient apiClient, Uri uri, HttpMethod method, Dictionary<string, object> providedParameters, bool auth, ArrayParametersSerialization arraySerialization, HttpMethodParameterPosition parameterPosition, out SortedDictionary<string, object> uriParameters, out SortedDictionary<string, object> bodyParameters, out Dictionary<string, string> headers)
    {
        uriParameters = parameterPosition == HttpMethodParameterPosition.InUri ? new SortedDictionary<string, object>(providedParameters) : new SortedDictionary<string, object>();
        bodyParameters = parameterPosition == HttpMethodParameterPosition.InBody ? new SortedDictionary<string, object>(providedParameters) : new SortedDictionary<string, object>();
        headers = new Dictionary<string, string>();

        if (!auth)
            return;

        uri = uri.SetParameters(uriParameters, arraySerialization);
        if (uri.AbsoluteUri.Contains("/v1/"))
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

            headers.Add("X-PCK", Credentials.Key!.GetString());
            headers.Add("X-Stamp", nonce.ToString());
            headers.Add("X-Signature", sign);
        }
    }

}