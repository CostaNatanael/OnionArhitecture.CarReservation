using MediatR;
using OnionArhitecture.CarReservation.Application.Common.Exceptions;
using OnionArhitecture.CarReservation.Application.Common.Interfaces;
using OnionArhitecture.CarReservation.Domain.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnionArhitecture.CarReservation.Application.Cars.Commands.DeleteCarCommand
{
    public record DeleteCarCommand(int Id, string Prefix) : IRequest;
    

    public class DeleteCarCommandHandler : IRequestHandler<DeleteCarCommand>
    {

        private readonly IApplicationDbContext _context;

        public DeleteCarCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteCarCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Cars
            .FindAsync(new object[] { request.Prefix, request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Car), request.Id);
            }

            _context.Cars.Remove(entity);

            //TODO should raise event
           // entity.AddDomainEvent(new CarDeletedEvent(entity));

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
