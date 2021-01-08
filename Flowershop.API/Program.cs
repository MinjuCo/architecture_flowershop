using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Flowershop.API
{
    public class Program
    {
        //This starts the webserver
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            //logging
            .ConfigureLogging(x => 
            {
                x.ClearProviders();
                x.AddConsole();
            })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //Indicate further configuration can be found in the startup file.
                    webBuilder.UseStartup<Startup>();
                });
    }
}
