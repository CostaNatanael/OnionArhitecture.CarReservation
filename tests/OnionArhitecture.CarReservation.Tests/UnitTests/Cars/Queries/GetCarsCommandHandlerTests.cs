using FluentAssertions;
using OnionArhitecture.CarReservation.Application.Common.Interfaces;
using OnionArhitecture.CarReservation.Application.Reservation.Queries;
using OnionArhitecture.CarReservation.Tests.UnitTests.Mocks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnionArhitecture.CarReservation.Tests.UnitTests.Cars.Queries
{
    public class GetCarsCommandHandlerTests
    {
        private readonly IApplicationDbContext _mockDbContext;

        public GetCarsCommandHandlerTests()
        {
            _mockDbContext = MockApplicationDbContext.GetDbContext().Object;
        }


        [Fact]
        public async Task GetAllCars_ReturnsCarListTest()
        {
            var handler = new GetCarsQueryHandler(_mockDbContext);
            var result = await handler.Handle(new GetCarsQuery(), CancellationToken.None);

            Assert.IsType<List<CarDto>>(result);
            result.Should().HaveCount(3);
          
        }
    }
}
