using Autofac.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Serilog;

namespace POC.SITAzure.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /***
             * GET CONFIG SETTINGS
             */
            var configBuilder = new ConfigurationBuilder()
                                     .SetBasePath(Directory.GetCurrentDirectory())
                                     .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
                                     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            IConfiguration config = configBuilder.Build();

            System.Diagnostics.Debug.WriteLine("LOG FILE: " + config["Logging:FilePath"]);

            /***
             * CREATE Serilog logger
             */
            Log.Logger = new LoggerConfiguration()
                           .ReadFrom.Configuration(config)
                           .Enrich.WithThreadId()
                           .Enrich.WithThreadName()
                           .CreateLogger();

            Log.Logger.Information("Global Serilog logger created...");

            try
            {
                Log.Logger.Information("Bootstrapping Web API...");
                // ASP.NET Core 3.0+:
                // The UseServiceProviderFactory call attaches the
                // Autofac provider to the generic hosting mechanism.
                var host = Host.CreateDefaultBuilder(args)
                    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                    .ConfigureWebHostDefaults(webHostBuilder =>
                    {
                        webHostBuilder
                          .UseContentRoot(Directory.GetCurrentDirectory())
                          .UseIISIntegration()
                          .UseStartup<Startup>();
                    }).ConfigureServices((ctx, sc) =>
                    {
                        sc.AddControllers().AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null); ;
                        sc.AddMvc().AddControllersAsServices();
                        sc.AddLogging(lb => lb.AddSerilog());

                        /***
                         * add Swagger
                         */
                        sc.AddSwaggerGen(options =>
                        {
                            options.SwaggerDoc("v1", new OpenApiInfo
                            {
                                Title = "POC.SITAzure WEB API 0",
                                Version = "v1"
                            });
                        });
                    })
                    .UseSerilog()
                    .Build();

                host.Run();
            }
            catch (Exception ex)
            {
                Log.Logger.Fatal(ex, "Unable to bootstrap Web API...");
            }
            finally
            {
                if (Log.Logger != null) { Log.CloseAndFlush(); }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
