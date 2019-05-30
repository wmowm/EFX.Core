using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using RPC.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tibos.HostedService.Services;

namespace Tibos.HostedService
{
    public class Hello : IHello
    {
        //暂停所有任务
        public async Task PauseAllAsync()
        {
                await Program.super_scheduler.PauseAll();
                Console.WriteLine("暂停所有任务");
        }


        //开启所有任务
        public async Task ResumeAllAsync()
        {
            await Program.super_scheduler.ResumeAll();
            Console.WriteLine("开启所有任务");
        }


        //暂停单个任务
        public async Task PauseJobAsync(string jobName,string jobGroup)
        {
            await Program.super_scheduler.PauseJob(JobKey.Create(jobName, jobGroup));
            Console.WriteLine("暂停单个任务");
        }


        //开启指定任务
        public async Task ResumeAsync(string jobName, string jobGroup)
        {
            await Program.super_scheduler.ResumeJob(JobKey.Create(jobName, jobGroup));
            Console.WriteLine("开启指定任务");
        }



        public string SayHello(string msg)
        {
            return msg;
        }
    }
}
