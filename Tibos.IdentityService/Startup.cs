using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Tibos.IdentityService
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
            var basePath = Directory.GetCurrentDirectory();
            InMemoryConfiguration.Configuration = this.Configuration;
            services.AddIdentityServer()
             //.AddDeveloperSigningCredential()
             .AddSigningCredential(new X509Certificate2(Path.Combine(basePath,
                 Configuration["Certificates:CerPath"]),
                 Configuration["Certificates:Password"]))
            .AddInMemoryIdentityResources(InMemoryConfiguration.GetIdentityResources())
            .AddTestUsers(InMemoryConfiguration.GetUsers().ToList())
            .AddInMemoryClients(InMemoryConfiguration.GetClients())
          
          .AddInMemoryApiResources(InMemoryConfiguration.GetApiResources());


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

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("default");
            app.UseIdentityServer();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
