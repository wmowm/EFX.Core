using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Test
{
    public interface Temp
    {
        void OutputMsg();
    }

    public class TempA : Temp
    {
        public void OutputMsg()
        {
            Console.WriteLine("AAAAAAAAAAAAAAAAAA");
        }
    }

    public class TempB : Temp 
    {
        public void OutputMsg()
        {
            Console.WriteLine("BBBBBBBBBBBBBBBBBBBBB");
        }
    }

    public static class fk 
    {
        public static void TestGetAssembly() 
        {
            var assembly =  Assembly.GetCallingAssembly();//获取当前程序集
            Temp tp = (Temp)assembly.CreateInstance("Test.TempA");//这里要写的格式为“命名空间.类名称”，切记！
            tp.OutputMsg();
        }
    }
}
