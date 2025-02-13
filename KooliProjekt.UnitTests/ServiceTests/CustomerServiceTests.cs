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
    public class CustomerServiceTests
    {
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly Mock<ICustomerRepository> _repositoryMock;
        private readonly CustomerService _customerService;

        public CustomerServiceTests()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _repositoryMock = new Mock<ICustomerRepository>();
            _customerService = new CustomerService(_uowMock.Object);

            _uowMock.SetupGet(r => r.CustomerRepository)
                    .Returns(_repositoryMock.Object);
        }

        [Fact]
        public async Task List_should_return_list_of_cars()
        {
            // Arrange
            var results = new List<Customer>
            {
                new Customer { Id = 1 },
                new Customer { Id = 2 }
            };
            var pagedResult = new PagedResult<Customer> { Results = results };
            _repositoryMock.Setup(r => r.List(It.IsAny<int>(), It.IsAny<int>()))
                           .ReturnsAsync(pagedResult);

            // Act
            var result = await _customerService.List(1, 10);

            // Assert
            Assert.Equal(pagedResult, result);
        }

        [Fact]
        public async Task Get_should_return_car_by_id()
        {
            // Arrange
            var customer = new Customer { Id = 1 };

            _repositoryMock.Setup(x => x.Get(customer.Id)).ReturnsAsync(customer);

            // Act
            var result = await _customerService.Get(customer.Id);

            // Assert
            Assert.Equal(customer, result);
        }

        [Fact]
        public async Task Save_should_add_new_car()
        {
            // Arrange
            var newCustomer = new Customer { Id = 0 };

            // Act
            await _customerService.Save(newCustomer);

            // Assert
            _repositoryMock.VerifyAll();
        }

        [Fact]
        public async Task Save_should_update_old_car()
        {
            // Arrange
            var existingCustomer = new Customer { Id = 1 };

            // Act
            await _customerService.Save(existingCustomer);

            // Assert
            _repositoryMock.VerifyAll();
        }

        [Fact]
        public async Task Delete_should_remove_car()
        {
            // Arrange
            var customer = new Customer { Id = 1 };

            _repositoryMock.Setup(x => x.Delete(It.Is<int>(id => id == customer.Id)))
               .Returns(Task.CompletedTask);
            // Act
            await _customerService.Delete(customer.Id);

            // Assert
            _repositoryMock.VerifyAll();
        }

        [Fact]
        public async Task Lookup_should_return_fullname()
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer { Id = 1, FirstName = "Mati", LastName = "Maasikas" },
                new Customer { Id = 2, FirstName = "Anna", LastName = "Kivi" },
                new Customer { Id = 3, FirstName = "Kati", LastName = "Kask" },
            };

            _repositoryMock.Setup(repo => repo.Lookup())
                           .ReturnsAsync(customers
                           .OrderBy(c => c.LastName)
                           .ThenBy(c => c.FirstName)
                           .ToList());

            // Act
            var result = await _customerService.Lookup();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);

            // Check if customers are correctly ordered by LastName and FirstName
            Assert.Equal("Kask", result[0].LastName);
            Assert.Equal("Kivi", result[1].LastName);
            Assert.Equal("Maasikas", result[2].LastName);

            Assert.Equal("Kati", result[0].FirstName);
            Assert.Equal("Anna", result[1].FirstName);
            Assert.Equal("Mati", result[2].FirstName);
        }

    }
}
