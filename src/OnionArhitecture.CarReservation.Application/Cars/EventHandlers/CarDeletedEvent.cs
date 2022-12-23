using System;
using System.Collections.Generic;
using System.Text;

namespace OnionArhitecture.CarReservation.Domain.Tasks.Events
{
    public class CarDeletedEvent : ReservationEvent
    {
        public CarDeletedEvent(Guid id)
        {
            Id = id;
        }
    }
}
