using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tibos.WebAPI.Common
{
    public static class MsgService
    {
        public static void GetTickCount(this IApplicationBuilder app, IApplicationLifetime lifetime)
        {
            using (var consulClient = new ConsulClient(c => c.Address = new Uri("http://193.112.104.103:8800")))
            {
                var services = consulClient.Agent.Services().Result.Response.Values.Where(s => s.Service.Equals("Tibos.API", StringComparison.OrdinalIgnoreCase));
                if (!services.Any())
                {
                    Console.WriteLine("找不到服务的实例");
                }
                else
                {
                    var service = services.ElementAt(Environment.TickCount % services.Count());
                    Console.WriteLine($"{service.Address}:{service.Port}");
                }
            }

        }
    }
}
