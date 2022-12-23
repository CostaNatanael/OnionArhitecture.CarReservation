using FluentValidation;
using System;

namespace OnionArhitecture.CarReservation.Application.Cars.Commands.CreateCarCommand
{
    public class BookACarCommandValidator : AbstractValidator<BookACarCommand>
    {

        public BookACarCommandValidator()
        {
            RuleFor(v => v.DurationMin)
                    .InclusiveBetween(1, 120)
                    .NotEmpty()
                    .WithMessage("Please ensure that you have entered the duration in minutes. It should be between 1 and 120");
            RuleFor(v => v.StartTime).InclusiveBetween(DateTime.UtcNow, DateTime.UtcNow.AddHours(24))
                    .NotEmpty()
                    .WithMessage("Please ensure that The start time of the reservation is not more than 24 hours from now and its not in the past");


        }
    }
}
