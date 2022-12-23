using OnionArhitecture.CarReservation.Domain.Tasks.ValueObjects;
using System.Collections.Generic;

namespace OnionArhitecture.CarReservation.Domain.Reservation
{
    public class Car : IAggregateRoot
    {

        /// <summary>
        /// Used as a composite key
        /// </summary>
        public int CarId { get; set; }

        /// <summary>
        /// Used as a composite key
        /// </summary>
        public string CarPrefix { get; set; } = "C";

        /// <summary>
        /// computed column => doesnt work with in-memory db . https://github.com/dotnet/efcore/issues/11032
        /// </summary>
       // public string Id { get; set; }


        /// <summary>
        /// The brand of the car
        /// </summary>
        public Make Make { get; set; }

        /// <summary>
        /// The model of the car
        /// </summary>
        public Model Model { get; set; }


        public  ICollection<Booking> Bookings { get; private set; } = new List<Booking>();
    }
}
