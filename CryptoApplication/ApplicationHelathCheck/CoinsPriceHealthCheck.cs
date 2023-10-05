using CryptoApplication.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoApplication.ApplicationHelathCheck
{
    public class CoinsPriceHealthCheck : IHealthCheck
    {
        private readonly ServiceSettings _settings;
        public CoinsPriceHealthCheck(IOptions<ServiceSettings> settings)
        {
            _settings = settings.Value;
        }

        public async  Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
                 //These sends a remote call to check the network stability flow
            Ping ping = new Ping();
            //Search for get Ip address on chrome
            //then go to the websute
            var url = "172.67.68.79";
            // https://www.worldcoinindex.com imputr in these url to get the Ip

            var reply = await ping.SendPingAsync(url);
            if (reply.Status != IPStatus.Success)
            {
                return HealthCheckResult.Unhealthy();
            }
            return HealthCheckResult.Healthy();


        }
    }
}
