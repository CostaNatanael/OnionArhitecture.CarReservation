using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnionArhitecture.CarReservation.Application.Common.Exceptions;
using OnionArhitecture.CarReservation.Application.Common.Interfaces;
using OnionArhitecture.CarReservation.Application.Mappers;
using OnionArhitecture.CarReservation.Domain.Reservation;

namespace OnionArhitecture.CarReservation.Application.Cars.Commands.CreateCarCommand
{
#nullable enable
    public record UpdateCarCommand(string? Make, string? Model, int Id, string Prefix) : IRequest;

#nullable disable
    public class UpdateCarCommandHandler : IRequestHandler<UpdateCarCommand>
    {

        private readonly IApplicationDbContext _context;

        public UpdateCarCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
        {

            var car = await _context.Cars.FindAsync(new object[] { request.Prefix , request.Id}, cancellationToken);
           
            if (car == null)
            {
                throw new NotFoundException(nameof(Car), request.Id);
            }

            if (!string.IsNullOrEmpty(request.Model))
            {
                car.Model = new Domain.Tasks.ValueObjects.Model(request.Model);
            }
            if (!string.IsNullOrEmpty(request.Make))
            {
                car.Make = new Domain.Tasks.ValueObjects.Make(request.Make);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }


}
