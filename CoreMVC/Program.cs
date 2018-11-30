using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace CoreMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
        //.ConfigureAppConfiguration((context, builder) => {
        //    builder.AddApollo(builder.Build().GetSection("apollo")).AddDefault()
        //    .AddNamespace("application");
        //});
    }
}
