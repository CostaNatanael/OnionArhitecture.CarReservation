using Newtonsoft.Json;
using OnionArhitecture.CarReservation.Application.Reservation.Queries;
using System;

namespace OnionArhitecture.CarReservation.Application.ViewModels
{
    public record ReservationViewModel(CarDto car, DateTime start, DateTime end);


}
