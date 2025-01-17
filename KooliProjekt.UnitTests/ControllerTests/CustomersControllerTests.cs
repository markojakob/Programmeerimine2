using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using KooliProjekt.Services;
using KooliProjekt.Controllers;
using KooliProjekt.Data;
using Microsoft.AspNetCore.Mvc;
using KooliProjekt.Models;

namespace KooliProjekt.UnitTests.ControllerTests
{
    public class CustomersControllerTests
    {
        private readonly Mock<ICustomerService> _customersServiceMock;
        private readonly CustomersController _controller;

        public CustomersControllerTests() 
        {
            _customersServiceMock = new Mock<ICustomerService>();
            _controller = new CustomersController(_customersServiceMock.Object);
        }

        [Fact]
        public async Task Index_should_return_correct_view_with_data()
        {
            // Arrange
            int page = 1;
            var data = new List<Customer>
            {
                new Customer
                {
                    FirstName = "Mati",
                    LastName = "Maasikas",
                    PhoneNum = 57934854,
                    Address = "Pärnu"
                }
            };

            var pagedResult =   new PagedResult<Customer> { Results = data };
            _customersServiceMock.Setup(x => x.List(page, It.IsAny<int>(), null)).ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.Index(page) as ViewResult;
            var model = (CustomersIndexModel)result.Model;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(pagedResult, model.Data);

        }
    }
}
