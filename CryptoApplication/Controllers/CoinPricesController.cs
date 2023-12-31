﻿using CryptoApplication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoApplication.Controllers
{
    //When i used these url which was the same as the one use in V2 i was having some funny 
    //error it seems was ab ug from .net s ichanged the name 
  //  [Route("api/[controller]")]
    [Route("api/v1/[controller]")]
    [ApiController]
    [ApiVersion("1.0", Deprecated =true)]
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
//Gdp, Usd