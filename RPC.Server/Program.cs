using Coldairarrow.DotNettyRPC;
using System;
using RPC.Contract;
namespace RPC.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("我是服务端:");
            RPCServer rPCServer = new RPCServer(9999);
            rPCServer.RegisterService<IHello, Hello>();
            rPCServer.Start();

            Console.ReadLine();
        }
    }
}
