using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoApplication.Services
{
   public  interface IApiClient
    {
        CoinsInfo GetCryptoCurrencyPrice(string currency);
    }
}
