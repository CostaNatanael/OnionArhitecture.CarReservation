using Moq;
using OnionArhitecture.CarReservation.Application.Cars.Commands.CreateCarCommand;
using OnionArhitecture.CarReservation.Application.Common.Interfaces;
using OnionArhitecture.CarReservation.Domain.Reservation;
using OnionArhitecture.CarReservation.Tests.UnitTests.Mocks;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using FluentValidation.TestHelper;
using OnionArhitecture.CarReservation.Application.Cars.Commands.DeleteCarCommand;

namespace OnionArhitecture.CarReservation.Tests.UnitTests.Cars.Commands
{
    public class DeleteCarCommendHandlerTests
    {
        private readonly Mock<IApplicationDbContext> _mockDbContext;
        public DeleteCarCommendHandlerTests()
        {
            _mockDbContext = MockApplicationDbContext.GetDbContext();
        }


        [Theory]
        [InlineData(2, "C")]
        public async Task DeleteCar_Success_RemovesTheCar(int id, string prefix)
        {        

            var handler = new DeleteCarCommandHandler(_mockDbContext.Object);

            var commend = new DeleteCarCommand(id, prefix);

            var result = await handler.Handle(commend, CancellationToken.None);

            _mockDbContext.Verify(x => x.Cars.Remove(It.IsAny<Car>()), Times.Exactly(1));
            _mockDbContext.Verify(m => m.SaveChangesAsync(CancellationToken.None), Times.Once);

        }

      
     
    }
}
