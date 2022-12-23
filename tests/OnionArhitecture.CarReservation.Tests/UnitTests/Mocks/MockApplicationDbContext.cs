using Moq;

using OnionArhitecture.CarReservation.Application.Common.Interfaces;
using OnionArhitecture.CarReservation.Domain.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using MockQueryable.Moq;
using System.Threading;

namespace OnionArhitecture.CarReservation.Tests.UnitTests.Mocks
{
    /// <summary>
    /// TODO should mock all async methods with all overloads
    /// </summary>

    public static class MockApplicationDbContext
    {
        public static Mock<IApplicationDbContext> GetDbContext()
        {
            var jetta = new Car { CarId = 1, CarPrefix = "C", Model = new Domain.Tasks.ValueObjects.Model("Jetta"), Make = new Domain.Tasks.ValueObjects.Make("VW") };
            var pollo = new Car { CarId = 2, CarPrefix = "C", Model = new Domain.Tasks.ValueObjects.Model("Polo"), Make = new Domain.Tasks.ValueObjects.Make("VW") };
            var sandero = new Car { CarId = 3, CarPrefix = "C", Model = new Domain.Tasks.ValueObjects.Model("Sandero"), Make = new Domain.Tasks.ValueObjects.Make("Dacia") };

            var cars = new List<Car> { jetta, pollo, sandero };
            var bookings = new List<Booking>
            {
              new Booking { Car=jetta , CarId = jetta.CarId , CarPrefix = jetta.CarPrefix , EndTime = DateTime.UtcNow.AddHours(20) , StartTime = DateTime.UtcNow.AddHours(18)}
            };

            var mockDbContext = new Mock<IApplicationDbContext>();

            var mockCars = cars.AsQueryable().BuildMockDbSet();

            var mockBooking = bookings.AsQueryable().BuildMockDbSet();

            mockDbContext.Setup(x => x.Cars).Returns(mockCars.Object);

            mockDbContext.Setup(x => x.Bookings).Returns(mockBooking.Object);


            mockDbContext.Setup(x => x.Cars.FindAsync(It.IsAny<object[]>(),It.IsAny<CancellationToken>())).ReturnsAsync((object[] ids, CancellationToken cancellationToken) =>
            {
                return cars.FirstOrDefault();
            });

            mockDbContext.Setup(x => x.Bookings.FindAsync(It.IsAny<object[]>(), It.IsAny<CancellationToken>())).ReturnsAsync((object[] ids, CancellationToken cancellationToken) =>
            {
                return bookings.FirstOrDefault();
            });



            //mockDbContext.Setup(m => m.Cars.FindAsync(It.IsAny<object[]>(), It.IsAny<CancellationToken>())).ReturnsAsync(cars.FirstOrDefault());

            //mockDbContext.Setup(m => m.Cars.FindAsync(It.IsAny<object[]>())).ReturnsAsync(cars.FirstOrDefault());
            //mockDbContext.Setup(m => m.Bookings.FindAsync(It.IsAny<object[]>(), It.IsAny<CancellationToken>())).ReturnsAsync(bookings.FirstOrDefault());
            //mockDbContext.Setup(m => m.Bookings.FindAsync(It.IsAny<object[]>())).ReturnsAsync(bookings.FirstOrDefault());


            //mockDbContext.Setup(c => c.Cars).ReturnsDbSet(cars);
            //mockDbContext.Setup(c => c.Bookings).ReturnsDbSet(bookings);
            return mockDbContext;
        }
    }

}
