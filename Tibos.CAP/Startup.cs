using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Tibos.CAP.Common;

namespace Tibos.CAP
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(typeof(ICacheService), new RedisCacheService(new ConfigurationOptions()
            {
                //AbortOnConnectFail = false,
                //AllowAdmin = true,
                //ConnectTimeout = 5000,
                //SyncTimeout = 2500,
                //ResponseTimeout = 15000,

                Password = "admin",//Redis数据库密码
                EndPoints = { "10.0.1.157:6379" }
                //EndPoints = { "127.0.0.1:6379"}
            }));

            services.AddCap(x =>
            {
                x.UseMySql("Data Source=10.0.1.157;Initial Catalog=fuluDB;port=3306; User ID=root;Password=fulu123;SslMode = none;");
                //x.UseKafka("132.232.4.73:9092");
                //x.UseRabbitMQ("10.0.1.157:15672");
                x.UseRabbitMQ(mq =>
                {
                    mq.HostName = "10.0.1.157";
                    mq.UserName = "admin";
                    mq.Password = "admin";
                }
                );
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            
            
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
