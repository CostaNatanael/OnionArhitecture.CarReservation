using Moq;
using OnionArhitecture.CarReservation.Application.Cars.Commands.CreateCarCommand;
using OnionArhitecture.CarReservation.Application.Common.Interfaces;
using OnionArhitecture.CarReservation.Domain.Reservation;
using OnionArhitecture.CarReservation.Tests.UnitTests.Mocks;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using FluentValidation.TestHelper;


namespace OnionArhitecture.CarReservation.Tests.UnitTests.Cars.Commands
{
    public class UpdateCarCommendHandlerTests
    {
        private readonly Mock<IApplicationDbContext> _mockDbContext;
        private readonly UpdateCarCommandValidator _validator = new();
        public UpdateCarCommendHandlerTests()
        {
            _mockDbContext = MockApplicationDbContext.GetDbContext();
        }


        [Theory]
        [InlineData("Nissan", "Qashqai", 2, "C")]
        public async Task UpdateCar_Success_Returns(string make, string model, int id, string prefix)
        {
            

            var handler = new UpdateCarCommandHandler(_mockDbContext.Object);

            var commend = new UpdateCarCommand(make, model, id, prefix);

            var result = await handler.Handle(commend, CancellationToken.None);

          
            _mockDbContext.Verify(m => m.SaveChangesAsync(CancellationToken.None), Times.Once);


        }

        [Theory]
        [InlineData("", "ccwhgplffejecnpxwmozlkqbgmamiyvzzybithdvslsdcdcmacnmusaouccdhxmpphjxtuzizvamrokkkmzwsrhztmbzesinsxykz", 1, "C")]
        public void CreateACar_LongProerty_ThrowsEx(string make, string model, int id, string prefix)
        {

            var commend = new UpdateCarCommand(make, model, id, prefix);

            Assert.Throws<ValidationTestException>(() =>
             _validator.TestValidate(commend).ShouldNotHaveValidationErrorFor(l => l.Model));

        }

     
    }
}
