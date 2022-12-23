using Microsoft.Extensions.DependencyInjection;
using OnionArhitecture.CarReservation.Application.Common.Interfaces;
using OnionArhitecture.CarReservation.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace OnionArhitecture.CarReservation.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("CarReservationDb"));


            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

            //services.AddScoped<ApplicationDbContextInitialiser>();

            return services;
        }
    }
}
