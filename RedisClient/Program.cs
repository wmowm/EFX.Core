using StackExchange.Redis;
using System;

namespace RedisClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //创建连接
            using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("193.112.104.103:6379,password=admin"))
            {
                ISubscriber sub = redis.GetSubscriber();

                //订阅名为 messages 的通道

                sub.Subscribe("messages", (channel, message) => {

                    //输出收到的消息
                    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {message}");
                });
                Console.WriteLine("已订阅 messages");
                Console.ReadKey();
            }
        }
    }
}
