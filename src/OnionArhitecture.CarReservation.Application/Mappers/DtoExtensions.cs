using OnionArhitecture.CarReservation.Application.Reservation.Queries;
using OnionArhitecture.CarReservation.Application.ViewModels;
using OnionArhitecture.CarReservation.Domain.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnionArhitecture.CarReservation.Application.Mappers
{
    internal static class DtoExtensions
    {
        public static Expression<Func<Car, CarDto>> ToCarDto() => car => new CarDto($"{car.CarPrefix}{car.CarId}", car.Model.ToString(), car.Make.ToString());
        public static CarDto ToDto(this Car car) => new CarDto($"{car.CarPrefix}{car.CarId}", car.Model.ToString(), car.Make.ToString());


        public static Expression<Func<Booking, ReservationViewModel>> ToBookingVm() => booking => new ReservationViewModel(booking.Car.ToDto(), booking.StartTime, booking.EndTime);


    }
}
