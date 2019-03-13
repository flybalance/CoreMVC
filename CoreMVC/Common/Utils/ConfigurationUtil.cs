using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Common.Utils
{
    public class ConfigurationUtil
    {
        public IConfiguration config { get; set; }

        public ConfigurationUtil()
        {
            IHostingEnvironment env = MyServiceProvider.ServiceProvider.GetRequiredService<IHostingEnvironment>();
            config = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public T GetAppSettings<T>(string key) where T : class, new()
        {
            var appconfig = new ServiceCollection()
                .AddOptions()
                .Configure<T>(config.GetSection(key))
                .BuildServiceProvider()
                .GetService<IOptions<T>>()
                .Value;

            return appconfig;
        }
    }

    //我比较喜欢单独放这个类，但是这样放更明显
    public class MyServiceProvider
    {
        public static IServiceProvider ServiceProvider { get; set; }
    }
}
