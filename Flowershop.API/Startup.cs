using System;
using System.Net.Http;
using System.Threading.Tasks;
using BasisRegisters.Vlaanderen;
using System.IO;
using System.Text.Json.Serialization;
using Flowershop.API.Database;
using Flowershop.API.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Extensions.Http;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Flowershop.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Notice you don't add IBasisRegisterService directly. Since you add a service for the HttpClient 
            // it depends on it's automatically added with the correct lifecycle.
            // HttpClients are pretty complex in .NET Core.
             services.AddHttpClient<IBasisRegisterService, BasisRegisterService>(
               (_, client) =>
                {
                    // needless to say, better in config. We pass the api baseuri here.
                    client.BaseAddress = new Uri("https://api.basisregisters.vlaanderen.be");
                })
                .AddPolicyHandler(GetRetryPolicy());

                services.AddControllers()
                .AddJsonOptions(options =>
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
                ;
            // Database connection with MySQL Pomelo
            // // https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql
            services.AddDbContextPool<FlowershopContext>(    
                dbContextOptions => dbContextOptions
                    .UseMySql(
                        // Replace with your connection string. Should be in your env but for example purposes this is _good enough_ for now
                        "server=localhost;user=root;password=;database=flowershop",
                        // Replace with your server version and type.
                        mySqlOptions => mySqlOptions
                            .ServerVersion(new Version(8, 0, 21), ServerType.MySql)
                            .CharSetBehavior(CharSetBehavior.NeverAppend))
                    // Everything from this point on is optional but helps with debugging.
                    .UseLoggerFactory(
                        LoggerFactory.Create(
                            logging => logging
                                .AddConsole()
                                .AddFilter(level => level >= LogLevel.Information)))
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors());
            services.AddTransient<IShopRepository, ShopRepository>();
            services.AddTransient<IBouquetRepository, BouquetRepository>();
            
            //Generate a swagger file
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Title = "Flowershop API", 
                    Version = "v1" 
                });

                var filePath = Path.Combine(System.AppContext.BaseDirectory, "Flowershop.API.xml");
                c.IncludeXmlComments(filePath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //Add a UI for swaggerUI
            app.UseSwagger();
            app.UseSwaggerUI(c => 
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Flowershop API V1");
            });
        }
        
        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            // https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/implement-http-call-retries-exponential-backoff-polly
            // You can just "read" this part of the code, it does what you think it does.
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2,
                    retryAttempt)));
        }

    }
}
