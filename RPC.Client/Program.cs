using Coldairarrow.DotNettyRPC;
using RPC.Contract;
using System;

namespace RPC.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("我是客户端:");
            IHello client = RPCClientFactory.GetClient<IHello>("127.0.0.1", 9999);
            var msg = client.SayHello("Hello");
            Console.WriteLine(msg);
            Console.ReadLine();
        }
    }
}
