using OnionArhitecture.CarReservation.Domain.Tasks.Events;
using System.Threading.Tasks;

namespace OnionArhitecture.CarReservation.Application.Handlers
{
    public class CarEventHandler
    {
        public async Task HandleCarCreatedEvent(ReservationCreatedEvent taskCreatedEvent)
        {
           
        }

        public async Task HandleCarDeletedEvent(CarDeletedEvent taskDeletedEvent)
        {
            // Here you can do whatever you need with this event, you can propagate the data using a queue, calling another API or sending a notification or whatever
            // With this scenario, you are building a event driven architecture with microservices and DDD
        }
    }
}
