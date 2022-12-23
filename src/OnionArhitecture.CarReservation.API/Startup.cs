using System.Reflection;
using OnionArhitecture.CarReservation.API.Extensions.Middleware;
using OnionArhitecture.CarReservation.Application.Handlers;
using Jaeger;
using Jaeger.Samplers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTracing;
using OpenTracing.Util;
using Serilog;
using OnionArhitecture.CarReservation.Infrastructure;
using FluentValidation.AspNetCore;

namespace OnionArhitecture.CarReservation.API
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

            services.AddInfrastructureServices();
            services.AddApplicationServices();

#pragma warning disable CS0618 // Type or member is obsolete
            services.AddControllers()
                    .AddFluentValidation();
#pragma warning restore CS0618
            // Type or member is obsolete
                              //.AddJsonOptions(options =>
                              //{
                              //    options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
                              //})
            ;

            services.AddScoped<CarEventHandler>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();



            services.AddSingleton(serviceProvider =>
            {
                var serviceName = Assembly.GetEntryAssembly().GetName().Name;

                var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

                ISampler sampler = new ConstSampler(true);

                ITracer tracer = new Tracer.Builder(serviceName)
                    .WithLoggerFactory(loggerFactory)
                    .WithSampler(sampler)
                    .Build();

                GlobalTracer.Register(tracer);

                return tracer;
            });

            Log.Logger = new LoggerConfiguration().CreateLogger();

            services.AddOpenTracing();

            services.AddOptions();

            services.AddSwaggerGen(
                //options =>
                //options.MapType<DateTime>(() => new OpenApiSchema
                //{
                //    Type = "string",
                //    Format = "date",
                //    Example = new OpenApiString("Dec 22, 2022 7:20:12 PM EET")
                //})
                );
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

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Car Reservation API V1");
            });


        }
    }
}
