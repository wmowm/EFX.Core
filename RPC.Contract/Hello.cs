using System;
using System.Threading.Tasks;

namespace RPC.Contract
{
    public class Hello : IHello
    {
        public async Task PauseAllAsync()
        {
            Console.WriteLine("test");
        }

        public Task PauseJobAsync(string jobName, string jobGroup)
        {
            throw new NotImplementedException();
        }

        public Task ResumeAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task ResumeAsync(string jobName, string jobGroup)
        {
            throw new NotImplementedException();
        }

        public string SayHello(string msg)
        {
            return msg;
        }
    }
}
