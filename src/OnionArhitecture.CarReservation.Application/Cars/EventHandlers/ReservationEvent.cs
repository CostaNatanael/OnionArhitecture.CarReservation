using System;
namespace OnionArhitecture.CarReservation.Domain.Tasks.Events
{
    public class ReservationEvent
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public string Summary { get; set; }
    }
}
