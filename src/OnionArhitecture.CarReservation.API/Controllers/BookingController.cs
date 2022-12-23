using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnionArhitecture.CarReservation.Application.Cars.Commands.CreateCarCommand;
using OnionArhitecture.CarReservation.Application.Reservation.Queries;
using OnionArhitecture.CarReservation.Application.ViewModels;

namespace OnionArhitecture.CarReservation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ApiControllerBase
    {

        /// <summary>
        /// Gets all the reservations
        /// </summary>
        /// <returns></returns>

        [HttpGet()]
        public async Task<ActionResult<List<ReservationViewModel>>> GetAllReservations()
        {
            return await Mediator.Send(new GetAllBookingsQuery());
        }


        /// <summary>
        /// Reserves a new car. The Start Time is in UTC and needs to be bigger than the current time. The duration cannot exceed 120 min
        /// </summary>
        /// <param name="command">The properties of the car
        /// </param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ReservationViewModel>> ReserveACar(BookACarCommand command)
        {
            return await Mediator.Send(command);
        }
    
    }
}