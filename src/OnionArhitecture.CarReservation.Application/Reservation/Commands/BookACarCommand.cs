using MediatR;
using Microsoft.EntityFrameworkCore;
using OnionArhitecture.CarReservation.Application.Common.Exceptions;
using OnionArhitecture.CarReservation.Application.Common.Interfaces;
using OnionArhitecture.CarReservation.Application.Mappers;
using OnionArhitecture.CarReservation.Application.ViewModels;
using OnionArhitecture.CarReservation.Domain.Reservation;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnionArhitecture.CarReservation.Application.Cars.Commands.CreateCarCommand
{
    public record BookACarCommand : IRequest<ReservationViewModel>
    {
        public DateTimeOffset StartTime { get; set; }

        public int DurationMin { get; set; }
    }

    public class BookACarCommandHandler : IRequestHandler<BookACarCommand, ReservationViewModel>
    {

        private readonly IApplicationDbContext _context;

        public BookACarCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ReservationViewModel> Handle(BookACarCommand request, CancellationToken cancellationToken)
        {
            var reservationEndTIme = request.StartTime.UtcDateTime.AddMinutes(request.DurationMin);
            var reservationStartTime = request.StartTime.UtcDateTime;

            var cars = _context.Cars
                .Include(x => x.Bookings)
                .ToList();

 
            var availableCar = await _context.Cars
                .FirstOrDefaultAsync(x => !x.Bookings.Any() || !x.Bookings.Any(one =>  reservationEndTIme > one.StartTime && reservationStartTime < one.EndTime),
                cancellationToken: cancellationToken);


            if (availableCar == null)
            {
                throw new NotFoundException("Could not find an available Car");
            }


            var reservation = new Booking
            {
                Car = availableCar,
                StartTime = reservationStartTime,
                EndTime = reservationEndTIme
            };

            await _context.Bookings.AddAsync(reservation);
            await _context.SaveChangesAsync(cancellationToken);

            return new ReservationViewModel(availableCar.ToDto(), reservation.StartTime, reservation.EndTime);

        }
    }


}
