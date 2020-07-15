using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Persistance;
using MediatR;

namespace Costify
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
             using(var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                IMediator mediator = services.GetService<IMediator>();

                try{
                    await (new SeedCost(mediator)).Seed();
                }
                catch(Exception ex)
                {
                    var logger = host.Services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex,"An error occured seeding the DB.");
                }
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
