using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CoreMVC
{
    public class Program
    {
        private static IConfiguration configuration { get; set; }

        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("Config\\host.json", optional: true, reloadOnChange: true)
             .AddEnvironmentVariables();
            configuration = builder.Build();
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args).UseConfiguration(configuration)
                 .UseStartup<Startup>();
        }
        //.ConfigureAppConfiguration((context, builder) => {
        //    builder.AddApollo(builder.Build().GetSection("apollo")).AddDefault()
        //    .AddNamespace("application");
        //});
    }
}
