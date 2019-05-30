using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.IdentityModel.Protocols;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tibos.CAP.Common
{
    public class RedisCacheService : ICacheService
    {
        int Default_Timeout = 600;//默认超时时间（单位秒）
        JsonSerializerSettings jsonConfig = new JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore, NullValueHandling = NullValueHandling.Ignore };
        ConnectionMultiplexer connectionMultiplexer;
        IDatabase database;

        class CacheObject<T>
        {
            public int ExpireTime { get; set; }
            public bool ForceOutofDate { get; set; }
            public T Value { get; set; }
        }

        public RedisCacheService(ConfigurationOptions options)
        {
            //设置线程池最小连接数
            ThreadPool.SetMinThreads(200, 200);

            connectionMultiplexer = ConnectionMultiplexer.Connect(options);
            database = connectionMultiplexer.GetDatabase();
        }


        public IDatabase GetDatabase()
        {
            return database;
        }

        /// <summary>
        /// 连接超时设置
        /// </summary>
        public int TimeOut
        {
            get
            {
                return Default_Timeout;
            }
            set
            {
                Default_Timeout = value;
            }
        }

        public string Get(string key)
        {
            return database.StringGet(key);
        }

        public T Get<T>(string key)
        {
            var cacheValue = database.StringGet(key);

            var res = JsonConvert.DeserializeObject<T>(cacheValue);
            return res;
        }

        public void Set(string key, object data)
        {
            database.StringSet(key, JsonConvert.SerializeObject(data));
        }

        public void Set(string key, object data, int cacheTime)
        {
            var timeSpan = TimeSpan.FromSeconds(cacheTime);
            database.StringSet(key, JsonConvert.SerializeObject(data), timeSpan);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            database.KeyDelete(key, CommandFlags.HighPriority);
        }

        /// <summary>
        /// 判断key是否存在
        /// </summary>
        public bool Exists(string key)
        {
            return database.KeyExists(key);
        }
    }
}
