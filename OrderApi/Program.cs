using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OrderApi.Data;

namespace OrderApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
           var host =  CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var serviceproviders = scope.ServiceProvider;
                var context = serviceproviders.GetRequiredService<OrdersContext>();
                MigrateDatabase.EnsureCreated(context);
            }
                 host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
