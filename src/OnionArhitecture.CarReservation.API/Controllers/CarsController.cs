using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnionArhitecture.CarReservation.Application.ViewModels;
using OnionArhitecture.CarReservation.Application.Cars.Commands.CreateCarCommand;
using OnionArhitecture.CarReservation.Application.Cars.Commands.DeleteCarCommand;
using OnionArhitecture.CarReservation.Application.Reservation.Queries;

namespace OnionArhitecture.CarReservation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ApiControllerBase
    {
        /// <summary>
        /// Gets all the cars
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public async Task<ActionResult<List<CarDto>>> GetAllCars()
        {
            return await Mediator.Send(new GetCarsQuery());
        }


        /// <summary>
        /// Creates a new car
        /// </summary>
        /// <param name="command">The properties of the car</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<string>> Create(CreateCarCommand command)
        {
            return await Mediator.Send(command);
        }
        /// <summary>
        /// Updates the characteristics of a car
        /// </summary>
        /// <param name="id">The id of the car</param>
        /// <param name="command">the properties to be updated</param>
        /// <param name="prefix">Defaults to "C"</param>
        /// <returns></returns>

        [HttpPut("{id}/{prefix?}")]
        public async Task<ActionResult> Update(int id, UpdateCarCommendViewModel command, string prefix = "C")
        {

            var updateCommend = new UpdateCarCommand(command.Make, command.Model, id, prefix);

            await Mediator.Send(updateCommend);

            return NoContent();
        }
        /// <summary>
        /// Deletes a car
        /// </summary>
        /// <param name="id">The Id of the car</param>
        /// <param name="prefix">Defaults to "C"</param>
        /// <returns></returns>

        [HttpDelete("{id}/{prefix?}")]
        public async Task<ActionResult> Delete(int id, string prefix = "C")
        {
            await Mediator.Send(new DeleteCarCommand(id, prefix));

            return NoContent();
        }
    }
}