using Autofac;
using Autofac.Extras.DynamicProxy;
using AutofacSerilogIntegration;
using Castle.DynamicProxy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using SITAzure.DAO;
using SITAzure.Interceptors;

namespace POC.SITAzure.Api
{
    public class Startup
    {
        public IContainer ApplicationContainer { get; private set; }

        public IConfigurationRoot Configuration { get; private set; }

        public ILifetimeScope AutofacContainer { get; private set; }

        public Startup(IWebHostEnvironment env)
        {
            // In ASP.NET Core 3.0 `env` will be an IWebHostingEnvironment, not IHostingEnvironment.
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            this.Configuration = builder.Build();
        }

        // ConfigureContainer is where you can register things directly
        // with Autofac. This runs after ConfigureServices so the things
        // here will override registrations made in ConfigureServices.
        // Don't build the container; that gets done for you by the factory.
        public void ConfigureContainer(ContainerBuilder builder)
        {
            string connInfo = this.Configuration.GetConnectionString("SITAzure2022");

            /***
             * Register Serilog
             */
            builder.RegisterLogger();

            /***
             * Register API Controllers
             */
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                                           .Where(asm => asm.Name.ToLower().EndsWith("controller"))
                                           .EnableClassInterceptors()
                                           .InterceptedBy(typeof(ApiControllerInterceptor))/**/;

            /***
             * Add DAL
             */
            builder.Register(ctx =>
            {
                var gnrtr = new ProxyGenerator();
                return gnrtr.CreateInterfaceProxyWithTargetInterface<IRepositoryOperations>
                (
                    new MultiDataRepositoryManager(connInfo),
                    new SITAsyncInterceptor(Log.Logger, "An exception was thrown in the data access layer. Please check the logs.")
                );
            })
            .AsSelf()
            .InstancePerLifetimeScope();

            /***
             * Register Interceptors
             */
            builder.RegisterType<ApiControllerInterceptor>().AsSelf();
            builder.RegisterType<SITAsyncInterceptor>().AsSelf();

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        /*
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestWebApi0", Version = "v1" });
            });
        }
        */

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "POC.SITAzure.Api v1"));
            }

            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
