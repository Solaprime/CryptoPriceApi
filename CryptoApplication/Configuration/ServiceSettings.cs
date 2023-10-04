using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoApplication.Configuration
{
    public class ServiceSettings
    {
        public string  CoinsPriceUrl { get; set; }
        public string  ApiKey { get; set; }
    }
}

// https://www.worldcoinindex.com/apiservice/ticker?key={key}&label=ethbtc-ltcbtc&fiat=btc
