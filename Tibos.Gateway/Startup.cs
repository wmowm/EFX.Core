using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;

namespace Tibos.Gateway
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
            var authenticationProviderKey = "OcelotKey";
            var identityServerOptions = new IdentityServerOptions();
            Configuration.Bind("IdentityServerOptions", identityServerOptions);
            services.AddAuthentication(identityServerOptions.IdentityScheme)
                .AddIdentityServerAuthentication(authenticationProviderKey, options =>
                {
                    options.RequireHttpsMetadata = false; //是否启用https
                    options.Authority = $"http://{identityServerOptions.ServerIP}:{identityServerOptions.ServerPort}";//配置授权认证的地址
                    options.ApiName = identityServerOptions.ResourceName; //资源名称，跟认证服务中注册的资源列表名称中的apiResource一致
                    options.SupportedTokens = SupportedTokens.Both;
                    options.ApiSecret = "secret";
                }
                );


            services.AddOcelot()//注入Ocelot服务
                    .AddConsul();


            //配置跨域处理
            services.AddCors(options =>
            {
                options.AddPolicy("default", builder =>
                {
                    builder.AllowAnyOrigin() //允许任何来源的主机访问
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();//指定处理cookie
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("default");
            app.UseAuthentication();

            //app.UseMvc(); -- no need MVC
            // Ocelot
            app.UseOcelot().Wait();
        }
    }
}
