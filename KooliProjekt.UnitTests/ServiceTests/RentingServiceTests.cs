using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using KooliProjekt.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class RentingServiceTests
    {
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly Mock<IRentingRepository> _repositoryMock;
        private readonly RentingService _rentingService;

        public RentingServiceTests()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _repositoryMock = new Mock<IRentingRepository>();
            _rentingService = new RentingService(_uowMock.Object);

            _uowMock.SetupGet(r => r.RentingRepository)
                    .Returns(_repositoryMock.Object);
        }


        [Fact]
        public async Task List_should_return_list_of_rentings()
        {
            // Arrange
            var results = new List<Renting>
            {
                new Renting { Id = 1 },
                new Renting { Id = 2 }
            };
            var pagedResult = new PagedResult<Renting> { Results = results };
            _repositoryMock.Setup(r => r.List(It.IsAny<int>(), It.IsAny<int>()))
                           .ReturnsAsync(pagedResult);

            // Act
            var result = await _rentingService.List(1, 10);

            // Assert
            Assert.Equal(pagedResult, result);
        }

        [Fact]
        public async Task Get_should_return_car_by_id()
        {
            // Arrange
            var renting = new   Renting { Id = 1 };

            _repositoryMock.Setup(x => x.Get(renting.Id)).ReturnsAsync(renting);

            // Act
            var result = await _rentingService.Get(renting.Id);

            // Assert
            Assert.Equal(renting, result);
        }

        [Fact]
        public async Task Save_should_add_new_car()
        {
            // Arrange
            var newrenting = new Renting { Id = 0 };

            // Act
            await _rentingService.Save(newrenting);

            // Assert
            _repositoryMock.VerifyAll();
        }

        [Fact]
        public async Task Save_should_update_old_car()
        {
            // Arrange
            var existingRenting = new Renting { Id = 1 };

            // Act
            await _rentingService.Save(existingRenting);

            // Assert
            _repositoryMock.VerifyAll();
        }

        [Fact]
        public async Task Delete_should_remove_car()
        {
            // Arrange
            var renting = new Renting { Id = 1 };

            _repositoryMock.Setup(x => x.Delete(It.Is<int>(id => id == renting.Id)))
               .Returns(Task.CompletedTask);
            // Act
            await _rentingService.Delete(renting.Id);

            // Assert
            _repositoryMock.VerifyAll();
        }


    }
}
