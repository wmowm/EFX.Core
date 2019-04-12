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
                EndPoints = { "193.112.104.103:6379" }
                //EndPoints = { "127.0.0.1:6379"}
            }));

            services.AddCap(x =>
            {
                x.UseMySql("Data Source=132.232.4.73;Initial Catalog=CAP_DB;port=3307; User ID=root;Password=As123456;SslMode = none;");
                //x.UseKafka("132.232.4.73:9092");
                x.UseRabbitMQ("148.70.88.40:15672");
                x.UseRabbitMQ(mq =>
                {
                    mq.HostName = "148.70.88.40";
                    mq.UserName = "guest";
                    mq.Password = "ghosts1t";
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
