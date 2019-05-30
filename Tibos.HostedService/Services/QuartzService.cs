using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tibos.HostedService.Services
{
    public class QuartzService : IHostedService
    {
        private readonly ILogger _logger;
        private readonly IScheduler _scheduler;

        public QuartzService(ILogger<QuartzService> logger, IScheduler scheduler)
        {
            _logger = logger;
            _scheduler = scheduler;
        }


        //开始
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("开始Quartz调度...");
            await _scheduler.Start(cancellationToken);
        }

        //结束
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("停止Quartz调度...");
            await _scheduler.Shutdown(cancellationToken);
        }

        //暂停所有任务
        public async Task PauseAllAsync()
        {
            await _scheduler.PauseAll();
        }
    }
}
