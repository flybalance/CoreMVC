using Autofac;
using Autofac.Extensions.DependencyInjection;
using CoreMVC.Dependency;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.IO;
using System.Reflection;

namespace CoreMVC
{
    public class Startup
    {
        public static ILoggerRepository repository { get; set; }
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;

            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("Config\\appsettings.json", optional: true, reloadOnChange: true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
               .AddJsonFile("Config\\host.json",optional:true,reloadOnChange:true)
               .AddEnvironmentVariables();
            Configuration = builder.Build();
            repository = LogManager.CreateRepository("NETCoreRepository");
            XmlConfigurator.Configure(repository, new FileInfo("Config\\log4net.config"));
        }

        private IConfiguration Configuration { get; }

        public IContainer ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //services.AddSingleton<IMemberService, MemberService>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // 实例化 AutoFac  容器
            var builder = new ContainerBuilder();

            // CoreMVC是继承接口的实现方法类库名称
            var assemblys = Assembly.Load("CoreMVC");
            // IDependency 是一个接口（所有要实现依赖注入的借口都要继承该接口）
            var baseType = typeof(IDependency);
            builder.RegisterAssemblyTypes(assemblys)
                              .Where(m => baseType.IsAssignableFrom(m) && m != baseType)
                              .AsImplementedInterfaces().InstancePerLifetimeScope();


            builder.Populate(services);
            ApplicationContainer = builder.Build();

            // 第三方IOC接管 core内置DI容器
            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"assest")),
                RequestPath = new PathString("/assest")
            });
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=File}/{action=Index}/{id?}");
            });
        }
    }
}
