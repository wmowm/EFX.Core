using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Tibos.Phantomjs
{
    public class WebHelp
    {
        public static string GetHtmlImage(string url, string picpath, int interval = 1000)
        {
            try
            {
                string path = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
                //win
                //ProcessStartInfo start = new ProcessStartInfo(path + @"Phantomjs\phantomjs.exe");//设置运行的命令行文件问ping.exe文件，这个文件系统会自己找到 
                //linux
                ProcessStartInfo start = new ProcessStartInfo("/home/phantomjs/bin/phantomjs");
                start.WorkingDirectory = path + @"Phantomjs";
                picpath = path + picpath;
                //设置命令参数
                string commond = string.Format("{0} {1} {2} {3}", path + @"Phantomjs/screenshot.js", url, picpath, interval);
                start.Arguments = commond;
                StringBuilder sb = new StringBuilder();
                start.CreateNoWindow = true;//不显示dos命令行窗口 
                start.RedirectStandardOutput = true;// 
                start.RedirectStandardInput = true;// 
                start.UseShellExecute = false;//是否指定操作系统外壳进程启动程序 
                Process p = Process.Start(start);

                StreamReader reader = p.StandardOutput;//截取输出流                
                string line = reader.ReadToEnd();//每次读取一行 
                string strRet = line;// sb.ToString();
                p.WaitForExit();//等待程序执行完退出进程 
                p.Close();//关闭进程  
                reader.Close();//关闭流 
                return strRet;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
    }
}
