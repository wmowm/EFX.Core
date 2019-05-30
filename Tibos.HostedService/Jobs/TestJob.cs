using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tibos.HostedService.Jobs
{
    public class TibosJob : IJob
    {
        private readonly ILogger _logger;

        public TibosJob(ILogger<TestJob> logger)
        {
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"我是任务Test:{DateTime.Now}开始执行任务任务执行*****************************************");
            return Task.CompletedTask;
        }
    }
}
