using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Tibos.Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                            .UseStartup<Startup>()
                            .UseUrls("http://localhost:9500")
                            .ConfigureAppConfiguration((hostingContext, builder) =>
                            {
                                builder.AddJsonFile("configuration.json", false, true);
                            })
                            .Build();
        }
    }
}
