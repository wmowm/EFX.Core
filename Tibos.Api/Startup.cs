using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Tibos.Api.Controllers;
using Tibos.ConfingModel.model;
using Tibos.Confing.autofac;
using Tibos.Api.Areas.User.Controllers;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Tibos.Api.Filters;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Text;
using NLog.Extensions.Logging;
using NLog.Web;
using System.Reflection;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;
using AutoMapper;

namespace Tibos.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true) //默认
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)//增加环境配置文件，新建项目默认有
                //.AddJsonFile(env.ContentRootPath + @"\bin\Debug\netcoreapp2.0\application\ManageConfig.json", optional: true)//增加配置 (自定义配置路径)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }



        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //替换控制器所有者
            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());

            //添加AutoMapper
            services.AddAutoMapper();

            services.AddMvc(options=>
            {
                options.Filters.Add(typeof(ResourceFilterAttribute));
                options.Filters.Add(typeof(ActionFilterAttribute));
                options.Filters.Add(typeof(ExceptionFilterAttribute));
                options.Filters.Add(typeof(ResultFilterAttribute));

            }).AddJsonOptions(options =>
                {
                    //忽略循环引用
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    //不使用驼峰样式的key
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    //设置时间格式
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                });
            services.AddTransient<HttpContextAccessor>();

            //缓存
            services.AddMemoryCache();

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

            //添加options
            services.AddOptions();
            services.Configure<ManageConfig>(Configuration.GetSection("ManageConfig"));

            //权限验证
            services.AddAuthorization();

            //swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v2.5", new Info
                {
                    Version = "v2.5",
                    Title = "Tibos接口文档",
                    Description = "RESTful API for Tibos",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "Tibos", Email = "505613913@qq.com", Url = "" }
                });

                //Set the comments path for the swagger json and ui.
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "Tibos.Api.xml");
                c.IncludeXmlComments(xmlPath);

                //  c.OperationFilter<HttpHeaderOperation>(); // 添加httpHeader参数
            });

            var containerBuilder = new ContainerBuilder();
            //模块化注入
            containerBuilder.RegisterModule<DefaultModule>();
            //属性注入控制器

            //containerBuilder.RegisterControllers(Assembly.GetExecutingAssembly());  //注入所有Controller
            containerBuilder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).PropertiesAutowired();

            containerBuilder.Populate(services);
            var container = containerBuilder.Build();



            return new AutofacServiceProvider(container);
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                   name: "default",
                   template: "{controller}/{action}/{id?}",
                   defaults: new { controller = "Home", action = "Index" });
            });
            app.UseAuthentication();
            loggerFactory.AddNLog();//添加NLog
            env.ConfigureNLog(AppContext.BaseDirectory + "config/nlog.config");//读取Nlog配置文件

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v2.5/swagger.json", "Tibos API V2.5");
                c.ShowExtensions();
            });
        }
    }
}
