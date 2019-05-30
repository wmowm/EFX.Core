using System;
using System.Diagnostics;

namespace Tibos.Phantomjs
{
    class Program
    {
        static void Main(string[] args)
        {
            var picpath = $"upload/{Guid.NewGuid()}.png";
            var res = WebHelp.GetHtmlImage(@"https://www.hao123.com/", picpath);
            Console.WriteLine(res);
            Console.Read();
        }
    }
}
