using FluentAssertions;
using OnionArhitecture.CarReservation.Application.Common.Interfaces;
using OnionArhitecture.CarReservation.Application.Reservation.Queries;
using OnionArhitecture.CarReservation.Application.ViewModels;
using OnionArhitecture.CarReservation.Tests.UnitTests.Mocks;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnionArhitecture.CarReservation.Tests.UnitTests.Reservation.Queries
{
    public class GetAllBookingsQueryTests
    {
        private readonly IApplicationDbContext _mockDbContext;

        public GetAllBookingsQueryTests()
        {
            _mockDbContext = MockApplicationDbContext.GetDbContext().Object;
        }


        [Fact]
        public async Task GetAllBookings_ReturnsCarListTest()
        {
            var handler = new GetAllBookingsQueryHandler(_mockDbContext);
            var result = await handler.Handle(new GetAllBookingsQuery(), CancellationToken.None);

            Assert.IsType<List<ReservationViewModel>>(result);
            result.Should().HaveCount(1);
        }
    }

}
