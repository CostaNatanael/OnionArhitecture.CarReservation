using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnionArhitecture.CarReservation.Application.Common.Interfaces;
using OnionArhitecture.CarReservation.Application.Mappers;

namespace OnionArhitecture.CarReservation.Application.Cars.Commands.CreateCarCommand
{
    public record CreateCarCommand : IRequest<string>
    {
        public string Make { get; init; }

        public string Model { get; init; }
    }

    public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand, string>
    {

        private readonly IApplicationDbContext _context;

        public CreateCarCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Reservation.Car
            {
                Make = new Domain.Tasks.ValueObjects.Make(request.Make),
                Model = new Domain.Tasks.ValueObjects.Model(request.Model)
            };

            //TODO should raise event
            //entity.AddDomainEvent(new TodoItemCreatedEvent(entity));

            await _context.Cars.AddAsync(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.ToDto().Id;

        }
    }


}
