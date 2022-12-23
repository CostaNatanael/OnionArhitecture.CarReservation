using FluentValidation;

namespace OnionArhitecture.CarReservation.Application.Cars.Commands.CreateCarCommand
{
    public class UpdateCarCommandValidator : AbstractValidator<UpdateCarCommand>
    {

        public UpdateCarCommandValidator()
        {
            RuleFor(v => v.Model)
                    .MaximumLength(100);


            RuleFor(v => v.Make)
                   .MaximumLength(100);
                 
        }
    }
}
