using FluentValidation;

namespace OnionArhitecture.CarReservation.Application.Cars.Commands.CreateCarCommand
{
    public class CreateCarCommandValidator : AbstractValidator<CreateCarCommand>
    {

        public CreateCarCommandValidator()
        {
            RuleFor(v => v.Model)
                    .MaximumLength(100)
                    .NotEmpty();

            RuleFor(v => v.Make)
                   .MaximumLength(100)
                   .NotEmpty();
        }
    }
}
