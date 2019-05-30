using Microsoft.Extensions.Configuration;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace Tibos.HostedService.Config
{
    /// <summary>
    /// 更多设置请参考：https://github.com/quartznet/quartznet/blob/master/src/Quartz/Impl/StdSchedulerFactory.cs
    /// </summary>
    public class QuartzOption
    {
        public QuartzOption(IConfiguration config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            var section = config.GetSection("quartz");
            section.Bind(this);
        }

        public Scheduler Scheduler { get; set; }

        public ThreadPool ThreadPool { get; set; }

        public Plugin Plugin { get; set; }

        public NameValueCollection ToProperties()
        {
            var properties = new NameValueCollection
            {
                ["quartz.serializer.type"]= "binary",
                //任务实例
                ["quartz.scheduler.instanceName"] = Scheduler?.InstanceName,
                //存储类型
                ["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX,Quartz",
                //表名前缀
                ["quartz.jobStore.tablePrefix"] = "QRTZ_",
                //驱动类型(mysql)
                ["quartz.jobStore.driverDelegateType"] = "Quartz.Impl.AdoJobStore.MySQLDelegate,Quartz",
                //数据源名称
                ["quartz.jobStore.dataSource"] = "myDS",
                //连接字符串
                ["quartz.dataSource.myDS.connectionString"] = "Server=localhost;Database=myDS;Uid=root;Pwd=123456",
                //版本
                ["quartz.dataSource.myDS.provider"] = "MySql"
            };
            return properties;
        }
    }

    public class Scheduler
    {
        public string InstanceName { get; set; }
    }

    public class ThreadPool
    {
        public string Type { get; set; }

        public string ThreadPriority { get; set; }

        public int ThreadCount { get; set; }
    }

    public class Plugin
    {
        public JobInitializer JobInitializer { get; set; }
    }

    public class JobInitializer
    {
        public string Type { get; set; }
        public string FileNames { get; set; }
    }
}
