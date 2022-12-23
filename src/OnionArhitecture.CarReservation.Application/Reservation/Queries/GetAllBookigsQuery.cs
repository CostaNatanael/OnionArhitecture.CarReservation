using MediatR;
using Microsoft.EntityFrameworkCore;
using OnionArhitecture.CarReservation.Application.Common.Interfaces;
using OnionArhitecture.CarReservation.Application.Mappers;
using OnionArhitecture.CarReservation.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnionArhitecture.CarReservation.Application.Reservation.Queries
{
    public record GetAllBookingsQuery : IRequest<List<ReservationViewModel>>;

    public class GetAllBookingsQueryHandler : IRequestHandler<GetAllBookingsQuery, List<ReservationViewModel>>
    {
        private readonly IApplicationDbContext _context;


        public GetAllBookingsQueryHandler(IApplicationDbContext context)
        {
            _context = context;

        }

        public async Task<List<ReservationViewModel>> Handle(GetAllBookingsQuery request, CancellationToken cancellationToken) =>
            await _context.Bookings
                    .Include(x => x.Car)
                    .AsNoTracking()
                    .Select(DtoExtensions.ToBookingVm())
                    .ToListAsync(cancellationToken);


    }
}
