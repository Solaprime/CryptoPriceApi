using CryptoApplication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoApplication.Controllers.V2
{
    //We can also version wit Query String
    //in the Htpp Header as well
    [Route("api/v{version:apiVersion}/[controller]")]
   // [Route("api/v{version:apiVersion}[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class CoinPricesController : ControllerBase
    {
        private readonly ILogger<CoinPricesController> _logger;
        private readonly IApiClient _apiClient;
        public CoinPricesController(ILogger<CoinPricesController> logger, IApiClient apiClient)
        {
            _logger = logger;
            _apiClient = apiClient;

        }
        [HttpGet("{currency}")]
        public IActionResult GetPrice(string currency)
        {
            var result = _apiClient.GetCryptoCurrencyPrice(currency);
            return Ok(result);
        }
    }
}
