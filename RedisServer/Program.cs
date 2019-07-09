using StackExchange.Redis;
using System;

namespace RedisServer
{
    class Program
    {
        static void Main(string[] args)
        {
            //创建连接
            using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("193.112.104.103:6379,password=admin"))
            {
                ISubscriber sub = redis.GetSubscriber();

                Console.WriteLine("请输入任意字符，输入exit退出");

                string input;

                do
                {
                    input = Console.ReadLine();
                    sub.Publish("messages", input);
                } while (input != "exit");
            }
        }
    }
}
