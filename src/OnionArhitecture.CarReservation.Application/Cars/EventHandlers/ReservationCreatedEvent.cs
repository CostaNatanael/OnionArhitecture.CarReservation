using System;

namespace OnionArhitecture.CarReservation.Domain.Tasks.Events
{
    public class ReservationCreatedEvent : ReservationEvent
    {
        public ReservationCreatedEvent(Guid id, string description, string summary)
        {
            Id = id;
            Summary = summary;
            Description = description;
        }
    }
}
