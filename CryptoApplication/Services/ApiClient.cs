using CryptoApplication.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace CryptoApplication.Services
{
    public class ApiClient : IApiClient
    {
        private readonly ServiceSettings _settings;
        private readonly ILogger<ApiClient> _logger;
        private readonly IConfiguration _config;

        private static readonly List<HttpStatusCode> invalidStatusCodes = new List<HttpStatusCode>
        {
             HttpStatusCode.BadRequest,
             HttpStatusCode.BadGateway,
             HttpStatusCode.GatewayTimeout,
             HttpStatusCode.ServiceUnavailable,
             HttpStatusCode.Forbidden,
             HttpStatusCode.GatewayTimeout,
             HttpStatusCode.InternalServerError
        }; 

        public ApiClient(IOptions<ServiceSettings> settings, ILogger<ApiClient> logger, IConfiguration config)
        {
            _logger = logger;
            _settings = settings.Value;
            _config = config;
        }
        public CoinsInfo GetCryptoCurrencyPrice(string currency)
        {
            string secret = _config["worldcoinindexApiKey"];
            var retryPolicy = Policy.HandleResult<IRestResponse>(resp => invalidStatusCodes.Contains(resp.StatusCode))
                .WaitAndRetry(10, i =>TimeSpan.FromSeconds(Math.Pow(2, 1)),
                 (result, TimeSpan, currentRetryRecount, context) =>
                 {
                     _logger.LogError($"Request has failed with a {result.Result.StatusCode}. waiting for {TimeSpan}" +
                         $"before next retry. This is the current retry Count is{currentRetryRecount} " +
                         $" ise for  context ");
                 }
                );
            var client = new RestClient($"{_settings.CoinsPriceUrl}/ticker");
            var request = new RestRequest(Method.GET);

            request.AddParameter("Key", secret, ParameterType.GetOrPost);
            request.AddParameter("label", "ethbtc-ltcbtc-btcbtc", ParameterType.GetOrPost);
            request.AddParameter("fiat", currency, ParameterType.GetOrPost);
           //Wrap it in the retryPolicy 
            var policyResponse = retryPolicy.ExecuteAndCapture(() =>
                {
                    return client.Get(request);
               });
            if (policyResponse != null)
            {
                var markets = JsonSerializer.Deserialize<CoinsInfo>(policyResponse.Result.Content);
                return markets;
            }
            else
            {
                return null;
            }

            //witjout the retry Poolicy normoal return flow
            //var response = client.Get(request);
            // var markets = JsonSerializer.Deserialize<CoinsInfo>(response);
            //return markets;

        }

    }
    public record Market(string Label, string Name, double Price);
    public record CoinsInfo(Market[] Markets);
}
