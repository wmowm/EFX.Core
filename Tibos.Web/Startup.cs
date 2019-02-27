using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Tibos.Web.Filters;
using Tibos.Web.Models;

namespace Web
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
            services.AddDistributedMemoryCache();

            services.AddScoped<IViewRenderService, ViewRenderService>();
            //权限验证
            services.AddAuthorization();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(30); //配置session的有效时间,单位秒
            });
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ResourceFilterAttribute));
                options.Filters.Add(typeof(ActionFilterAttribute));
                options.Filters.Add(typeof(ExceptionFilterAttribute));
                options.Filters.Add(typeof(ResultFilterAttribute));

            });


            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // ensure not change any return Claims from Authorization Server
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc"; // oidc => open ID connect
            })
            .AddCookie("Cookies")
            .AddOpenIdConnect("oidc", options =>
            {
                options.SignInScheme = "Cookies";
                options.Authority = $"http://localhost:5000";
                options.RequireHttpsMetadata = false; // please use https in production env
                options.ClientId = "cas.mvc.client.implicit";
                options.ResponseType = "id_token token"; // allow to return access token
            options.SaveTokens = true;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSession();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            string contentRoot = Directory.GetCurrentDirectory();
            app.UseStaticFiles().UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(contentRoot, "temp")),
                RequestPath = "/documents"
            });


            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                   name: "default",
                   template: "{controller}/{action}/{id?}",
                   defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
