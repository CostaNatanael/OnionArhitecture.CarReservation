using Moq;
using OnionArhitecture.CarReservation.Application.Cars.Commands.CreateCarCommand;
using OnionArhitecture.CarReservation.Application.Common.Interfaces;
using OnionArhitecture.CarReservation.Domain.Reservation;
using OnionArhitecture.CarReservation.Tests.UnitTests.Mocks;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using FluentValidation;
using FluentValidation.TestHelper;
using System.ComponentModel.DataAnnotations;


namespace OnionArhitecture.CarReservation.Tests.UnitTests.Cars.Commands
{
    public class CreateACarCommendHandlerTests
    {
        private readonly Mock<IApplicationDbContext> _mockDbContext;
        private readonly CreateCarCommandValidator _validator = new CreateCarCommandValidator();
        public CreateACarCommendHandlerTests()
        {
            _mockDbContext = MockApplicationDbContext.GetDbContext();
        }


        [Theory]
        [InlineData("Nissan", "Qashqai")]
        public async Task CreateACar_Success_ReturnsNewId(string make, string model)
        {
            var handler = new CreateCarCommandHandler(_mockDbContext.Object);

            var commend = new CreateCarCommand { Make = make, Model = model };

            var result = await handler.Handle(commend, CancellationToken.None);

            _mockDbContext.Verify(m => m.Cars.AddAsync(It.IsAny<Car>(), default), Times.Once);
            _mockDbContext.Verify(m => m.SaveChangesAsync(CancellationToken.None), Times.Once);
            Assert.StartsWith("C", result);

        }

        [Theory]
        [InlineData("", "whatever")]
        public void CreateACar_EmptyMakel_ThrowsEx(string make, string model)
        {

            var commend = new CreateCarCommand { Make = make, Model = model };

            Assert.Throws<ValidationTestException>(() =>
             _validator.TestValidate(commend).ShouldNotHaveValidationErrorFor(l => l.Make));

        }

        [Theory]
        [InlineData("whatever", "")]
        public void CreateACar_EmptyModel_ThrowsEx(string make, string model)
        {

            var commend = new CreateCarCommand { Make = make, Model = model };

            Assert.Throws<ValidationTestException>(() =>
             _validator.TestValidate(commend).ShouldNotHaveValidationErrorFor(l => l.Model));
        }
    }
}
