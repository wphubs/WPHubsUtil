using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace WPHubsUtil.App
{
    public class App
    {
        public static IConfiguration Configuration;

        public static void InitConfigure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((hostContext, services) =>
            {
                Configuration = hostContext.Configuration;
            });
        }
    }
}
