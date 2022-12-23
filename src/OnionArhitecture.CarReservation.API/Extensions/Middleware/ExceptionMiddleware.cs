using Microsoft.AspNetCore.Http;
using OnionArhitecture.CarReservation.Application.Common.Interfaces;
using Serilog;
using System;
using System.Net;
using System.Threading.Tasks;

namespace OnionArhitecture.CarReservation.API.Extensions.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                Log.Error($"Something went wrong, error: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            switch (exception)
            {
                case ICarReservationException:
                    return context.Response.WriteAsync(new ErrorDetails()
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError,
                        Message = exception.Message
                    }.ToString());
                default:
                    return context.Response.WriteAsync(new ErrorDetails()
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = "An error has occurred"
                    }.ToString());
            }
        }
    }
}
