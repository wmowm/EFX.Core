using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.IO;
using Microsoft.Extensions.Logging;
using Quartz.Spi;
using Tibos.HostedService.Jobs;
using Tibos.HostedService.Config;
using Quartz.Impl;
using Quartz;
using Coldairarrow.DotNettyRPC;
using RPC.Contract;
using Microsoft.Extensions.Hosting;
using Tibos.HostedService.Services;

namespace Tibos.HostedService
{
    class Program
    {
        public static IScheduler super_scheduler;
        public static void Main(string[] args)
        {
            var host = new HostBuilder()
                        .ConfigureHostConfiguration(configHost =>
                        {
                            configHost.SetBasePath(Directory.GetCurrentDirectory());
                            //configHost.AddJsonFile("hostsettings.json", true, true);
                            configHost.AddEnvironmentVariables("ASPNETCORE_");
                            //configHost.AddCommandLine(args);
                        })
                        .ConfigureAppConfiguration((hostContext, configApp) =>
                        {
                            configApp.AddJsonFile("appsettings.json", true);
                            //configApp.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", true);
                            configApp.AddEnvironmentVariables();
                            //configApp.AddCommandLine(args);
                        })
                        .ConfigureServices((hostContext, services) =>
                        {
                            services.AddLogging();
                            //services.AddHostedService<TimedHostedService>();
                            services.AddSingleton<IJobFactory, JobFactory>();
                            services.AddSingleton<IHello, Hello>();
                            services.AddSingleton(provider =>
                            {
                                var option = new QuartzOption(hostContext.Configuration);


                                var sf = new StdSchedulerFactory(option.ToProperties());
                                var scheduler = sf.GetScheduler().Result;
                                super_scheduler = scheduler;
                                ////3.创建计划
                                //var trigger = TriggerBuilder.Create()
                                //.WithSimpleSchedule(x => x.WithIntervalInSeconds(2).RepeatForever())//每两秒执行一次
                                //.Build();
                                ////4、创建任务
                                //var jobDetail = JobBuilder.Create<TibosJob>()
                                //                .WithIdentity("job", "group5")
                                //                .Build();
                                //scheduler.ScheduleJob(jobDetail, trigger);
                                scheduler.JobFactory = provider.GetService<IJobFactory>();
                                return scheduler;
                            });
                            services.AddHostedService<QuartzService>();

                            services.AddSingleton<TestJob, TestJob>();
                            services.AddSingleton<TibosJob, TibosJob>();


                            RPCServer rPCServer = new RPCServer(9988);
                            rPCServer.RegisterService<IHello, Hello>();
                            rPCServer.Start();
                            Console.WriteLine("服务端启动了");

                        })
                        .ConfigureLogging((hostContext, configLogging) =>
                        {
                            configLogging.AddConsole();
                            if (hostContext.HostingEnvironment.EnvironmentName == EnvironmentName.Development)
                            {
                                configLogging.AddDebug();
                            }
                        })
                        .UseConsoleLifetime()
                        .Build();

            host.Run();
            Console.ReadLine();
        }
    }
}
