using FluentValidation.TestHelper;
using Moq;
using OnionArhitecture.CarReservation.Application.Cars.Commands.CreateCarCommand;
using OnionArhitecture.CarReservation.Application.Common.Interfaces;
using OnionArhitecture.CarReservation.Domain.Reservation;
using OnionArhitecture.CarReservation.Tests.UnitTests.Mocks;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnionArhitecture.CarReservation.Tests.UnitTests.Reservation.Commands
{
    public class BookACarCommandTests
    {
        private readonly Mock<IApplicationDbContext> _mockDbContext;
        private readonly BookACarCommandValidator _validator = new BookACarCommandValidator();
        public BookACarCommandTests()
        {
            _mockDbContext = MockApplicationDbContext.GetDbContext();
        }


        [Theory]
        [InlineData(10)]
        [InlineData(20)]
        [InlineData(120)]
        public async Task BookACar_Success_ReturnsNewBooking(int duration)
        {
            var handler = new BookACarCommandHandler(_mockDbContext.Object);

            var commend = new BookACarCommand { StartTime = DateTime.UtcNow, DurationMin = duration };

            var result = await handler.Handle(commend, CancellationToken.None);

            _mockDbContext.Verify(m => m.Bookings.AddAsync(It.IsAny<Booking>(), default), Times.Exactly(1));
            _mockDbContext.Verify(m => m.SaveChangesAsync(CancellationToken.None), Times.Exactly(1));


        }

        [Theory]
        [InlineData(10)]
        public void CreateACar_ReservationFor48h_ThrowsEx(int duration)
        {

            var commend = new BookACarCommand { DurationMin = duration, StartTime = DateTime.UtcNow.AddHours(48) };
          
            Assert.Throws<ValidationTestException>(() =>
             _validator.TestValidate(commend).ShouldNotHaveValidationErrorFor(l => l.StartTime));

        }

        [Theory]
        [InlineData(121)]
        public void CreateACar_ReservationMoreThan120_ThrowsEx(int duration)
        {

            var commend = new BookACarCommand { DurationMin = duration, StartTime = DateTime.UtcNow.AddHours(23) };

            Assert.Throws<ValidationTestException>(() =>
             _validator.TestValidate(commend).ShouldNotHaveValidationErrorFor(l => l.DurationMin));

        }
    }
}
