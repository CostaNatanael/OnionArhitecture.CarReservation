using MediatR;
using OnionArhitecture.CarReservation.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnionArhitecture.CarReservation.Application.Mappers;

namespace OnionArhitecture.CarReservation.Application.Reservation.Queries
{

    public record GetCarsQuery : IRequest<List<CarDto>>;

    public class GetCarsQueryHandler : IRequestHandler<GetCarsQuery, List<CarDto>>
    {
        private readonly IApplicationDbContext _context;


        public GetCarsQueryHandler(IApplicationDbContext context)
        {
            _context = context;

        }

        public async Task<List<CarDto>> Handle(GetCarsQuery request, CancellationToken cancellationToken) =>
            await _context.Cars
                    .AsNoTracking()
                    .Select(DtoExtensions.ToCarDto())
                    .OrderBy(t => t.Id)
                    .ToListAsync(cancellationToken);


    }
}

