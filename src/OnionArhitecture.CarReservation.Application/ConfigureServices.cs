using System.Reflection;
using FluentValidation;
using MediatR;
using OnionArhitecture.CarReservation.Application.Cars.Commands.CreateCarCommand;
using OnionArhitecture.CarReservation.Domain.Reservation;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
          

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
           
            return services;
        }
    }
}