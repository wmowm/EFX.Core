using Coldairarrow.DotNettyRPC;
using RPC.Contract;
using System;

namespace RPC.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--------------------------------------------------------------------------------------");
            Console.WriteLine("欢迎测试任务调度");
            Console.WriteLine("指令说明:");
            Console.WriteLine("1.停止Tibos任务,2.停止Test任务");
            Console.WriteLine("3.启动Tibos任务,4.启动Test任务");
            Console.WriteLine("5.启动全部任务 ,6.停止全部任务");
            Console.WriteLine("--------------------------------------------------------------------------------------");
            IHello client = RPCClientFactory.GetClient<IHello>("127.0.0.1", 9988);
            var instruction = "";
            while (instruction != "0")
            {
                Console.WriteLine("请输入指令!");
                instruction = Console.ReadLine();
                switch (instruction)
                {
                    case "1":
                        client.PauseJobAsync("job", "group4");
                        break;
                    case "2":
                        client.PauseJobAsync("job4", "group4");
                        break;
                    case "3":
                        client.ResumeAsync("job", "group4");
                        break;
                    case "4":
                        client.ResumeAsync("job4", "group4");
                        break;
                    case "5":
                        client.ResumeAllAsync();
                        break;
                    case "6":
                        client.PauseAllAsync();
                        break;
                    default:
                        break;
                }
            }
            Console.WriteLine("游戏结束!");
            Console.ReadLine();
        }
    }
}
