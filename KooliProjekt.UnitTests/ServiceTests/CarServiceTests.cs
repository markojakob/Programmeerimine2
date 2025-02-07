using System;
using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using KooliProjekt.Services;
using Moq;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class CarServiceTests
    {
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly Mock<ICarRepository> _repositoryMock;
        private readonly CarService _carService;

        public CarServiceTests()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _repositoryMock = new Mock<ICarRepository>();
            _carService = new CarService(_uowMock.Object);

            _uowMock.SetupGet(r => r.CarRepository)
                    .Returns(_repositoryMock.Object);
        }

        [Fact]
        public async Task List_should_return_list_of_cars()
        {
            // Arrange
            var results = new List<Car>
            {
                new Car { Id = 1 },
                new Car { Id = 2 }
            };
            var pagedResult = new PagedResult<Car> { Results = results };
            _repositoryMock.Setup(r => r.List(It.IsAny<int>(), It.IsAny<int>()))
                           .ReturnsAsync(pagedResult);

            // Act
            var result = await _carService.List(1, 10);

            // Assert
            Assert.Equal(pagedResult, result);
        }

        [Fact]
        public async Task Get_should_return_car_by_id()
        {
            // Arrange
            var car = new Car { Id = 1 };

            _repositoryMock.Setup(x => x.Get(car.Id)).ReturnsAsync(car);

            // Act
            var result = await _carService.Get(car.Id);

            // Assert
            Assert.Equal(car, result);
        }

        [Fact]
        public async Task Save_should_add_new_car()
        {
            // Arrange
            var newCar = new Car { Id = 0, CarMaker = "Toyota", Model = "Corolla" };

            // Act
            await _carService.Save(newCar);
                
            // Assert
            _repositoryMock.VerifyAll();
        }

        [Fact]
        public async Task Save_should_update_old_car()
        {
            // Arrange
            var existingCar = new Car { Id = 1, CarMaker = "Ford", Model = "Focus" };

            // Act
            await _carService.Save(existingCar);

            // Assert
            _repositoryMock.VerifyAll();
        }

        [Fact]
        public async Task Delete_should_remove_car()
        {
            // Arrange
            var car = new Car { Id = 1, CarMaker = "Ford", Model = "Focus" };

            _repositoryMock.Setup(x => x.Delete(It.Is<int>(id => id == car.Id)))
               .Returns(Task.CompletedTask);
            // Act
            await _carService.Delete(car.Id);

            // Assert
            _repositoryMock.VerifyAll();
        }
    }
}