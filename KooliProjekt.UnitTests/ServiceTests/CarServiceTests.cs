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

            var result = await _carService.Get(car.Id);

            // Assert
            Assert.Equal(car, result);
        }

        [Fact]
        public async Task Save_should_save_car()
        {
            // Arrange
            var car = new Car
            {
                Id = 1,
            };

            // Act
            await _carService.Save(car);

            // Assert
            _repositoryMock.VerifyAll();    
            
        }

        
    }
}