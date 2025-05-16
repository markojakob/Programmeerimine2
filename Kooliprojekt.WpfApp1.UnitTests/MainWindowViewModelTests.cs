using KooliProjekt.PublicApi;
using Moq;
using System.Security.Policy;
using WpfApp1;
using Xunit;
namespace Kooliprojekt.WpfApp1.UnitTests
{
    public class MainWindowViewModelTests
    {
        private readonly Mock<IApiClient> _apiMock;
        private readonly MainWindowViewModel _vm;
        public MainWindowViewModelTests()
        {
            _apiMock = new Mock<IApiClient>();
            _vm = new MainWindowViewModel(_apiMock.Object);
        }


        [Fact]
        public async Task SaveCommand_saves_car_when_car_is_not_null()
        {
            // Arrange
            var car = new Car { Id = 0, CarMaker = "Test", Model = "ModelX" };
            _vm.SelectedItem = car;

            // Mock Save to return success
            _apiMock.Setup(x => x.Save(It.IsAny<Car>()))
                    .ReturnsAsync(new Result());

            // Mock List to return some data
            _apiMock.Setup(x => x.List())
                    .ReturnsAsync(new Result<List<Car>> { Value = new List<Car> { car } });

            // Act
            _vm.SaveCommand.Execute(car);

            // Wait for command to finish (not ideal but okay for testing)
            await Task.Delay(100);

            // Assert
            _apiMock.Verify(x => x.Save(It.IsAny<Car>()), Times.Once);
            _apiMock.Verify(x => x.List(), Times.Once);
            Assert.Contains(car, _vm.Lists);
        }


        [Fact]
        public async Task DeleteCommand_deletes_car_when_confirmDelete_is_true()
        {
            // Arrange
            var car = new Car { Id = 1, CarMaker = "Ford", Model = "Focus" };
            _vm.SelectedItem = car;
            _vm.Lists.Add(car);
            _vm.ConfirmDelete = c => true;

            _apiMock.Setup(x => x.Delete(car.Id))
                    .ReturnsAsync(new Result());

            // Act
            _vm.DeleteCommand.Execute(car);

            // Wait for command to finish
            await Task.Delay(100);

            // Assert
            _apiMock.Verify(x => x.Delete(car.Id), Times.Once);
            Assert.False(_vm.Lists.Contains(car));
            Assert.Null(_vm.SelectedItem);
        }



    }
}