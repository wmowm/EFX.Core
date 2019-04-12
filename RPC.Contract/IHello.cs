using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RPC.Contract
{
    public interface IHello
    {
        string SayHello(string msg);

        //暂停所有任务
        Task PauseAllAsync();

        //开启所有任务
        Task ResumeAllAsync();

        //暂停单个任务
        Task PauseJobAsync(string jobName, string jobGroup);

        //开启指定任务
        Task ResumeAsync(string jobName, string jobGroup);
    }
}
