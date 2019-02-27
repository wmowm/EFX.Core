using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Tibos.WebAPI.Common;

namespace Tibos.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // IdentityServer
            //services.AddMvcCore().AddAuthorization().AddJsonFormatters();
            //services.AddAuthentication(Configuration["Identity:Scheme"])
            //    .AddIdentityServerAuthentication(options =>
            //    {
            //        options.RequireHttpsMetadata = false; // for dev env
            //        options.Authority = $"http://{Configuration["Identity:IP"]}:{Configuration["Identity:Port"]}";
            //        options.ApiName = Configuration["Service:Name"]; // match with configuration in IdentityServer
            //});


            //配置跨域处理
            services.AddCors(options =>
            {
                options.AddPolicy("any", builder =>
                {
                    builder.AllowAnyOrigin() //允许任何来源的主机访问
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();//指定处理cookie
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("default");

            //app.UseAuthentication();
            app.UseMvc();

            ServiceEntity serviceEntity = new ServiceEntity
            {
                IP = "193.112.104.103",
                Port = 6001,
                ServiceName = "Tibos.API",
                ConsulIP = "193.112.104.103",
                ConsulPort = 8800
            };
            //app.RegisterConsul(lifetime, serviceEntity);
        }
    }
}
