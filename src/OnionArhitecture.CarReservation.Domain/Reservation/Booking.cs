using System;

namespace OnionArhitecture.CarReservation.Domain.Reservation
{
    public class Booking : IAggregateRoot
    {


        public int Id { get; set; }

        #region Foreign keys

        public string CarPrefix { get; set; }

        public int CarId { get; set; }
        #endregion

        /// <summary>
        /// The starttime of the reservation.
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// The ending of the reservation
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// The car for a specific reservation
        /// </summary>
        public virtual Car Car { get; set; }

    }

}
