namespace MedicineHandler
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;

    public static class Program
    {
        public static Task Main()
        {
            return CreateHostBuilder().RunConsoleAsync();
        }

        public static IHostBuilder CreateHostBuilder()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(System.AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("conf/appsettings.json")
                .Build();

            return Host
                .CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseDefaultServiceProvider(options => options.ValidateOnBuild = false)
                        .UseConfiguration(config)
                        .UseStartup<Startup>();
                });
        }
    }
}
