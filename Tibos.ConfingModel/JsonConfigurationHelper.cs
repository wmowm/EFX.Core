using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
namespace Tibos.ConfingModel
{
    public static class JsonConfigurationHelper
    {
        public static T GetAppSettings<T>(string fileName,string key) where T : class, new()
        {
            var baseDir = AppContext.BaseDirectory + "json/";
            //var indexSrc = baseDir.IndexOf("src");
            //var subToSrc = baseDir.Substring(0, indexSrc);
            var currentClassDir = baseDir;

            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(currentClassDir)
                .Add(new JsonConfigurationSource { Path =fileName, Optional = false, ReloadOnChange = true })
                .Build();
            var appconfig = new ServiceCollection()
                .AddOptions()
                .Configure<T>(config.GetSection(key))
                .BuildServiceProvider()
                .GetService<IOptions<T>>()
                .Value;
            return appconfig;
        }


        public static string GetJson(string jsonPath,string key)
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile(jsonPath).Build(); //json文件地址
            string s = config.GetSection(key).Value; //json某个对象
            return s;
        }
    }
}
