using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using AutoMapper;
using Shipment.Business;
using Serilog;
using Shipment.Common;
using Shipment.Models;
using System.IO;
using Shipment.Core;
using Shipment.API.Middleware;

namespace Shipment.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConfigureLog();
        }

        void ConfigureLog()
        {
            Log.Logger = new LoggerConfiguration()
               .ReadFrom.Configuration(Configuration)
                .WriteTo.File("Logs/Shipment.log", restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error)
               .CreateLogger();
            Serilog.Debugging.SelfLog.Enable(Console.Error);
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", false)
              .Build();
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddSingleton<IParcelManager, ParcelManager>();
            services.AddSingleton(typeof(IRepository<>), typeof(InMemoryRepository<>));
            services.AddOptions();

            services.AddCors(options =>
            {
                options.AddPolicy(name: "MyPolicy",
                    policy =>
                    {
                        var domainSection = Configuration.GetSection("Api:AllowedWebAppDomain");
                        string[] domains = domainSection.AsEnumerable().Where(x=>x.Value != null).Select(x=>x.Value).ToArray();
                        policy.AllowAnyHeader().AllowAnyMethod();
                        policy.WithOrigins((string[])domains);
                    });
            });

            services.AddControllers();
            services.AddSwaggerGen();
             
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseCors("MyPolicy");
            loggerFactory.AddSerilog();
            app.UseMiddleware<GlobalExceptionMiddleware>();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shipment API v1.0");
            });
        }
    }
}
