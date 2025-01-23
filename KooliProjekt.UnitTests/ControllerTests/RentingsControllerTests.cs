using Castle.Core.Resource;
using KooliProjekt.Controllers;
using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace KooliProjekt.UnitTests.ControllerTests
{
    public class RentingsControllerTests
    {       
        private readonly Mock<ICustomerService> _customerServiceMock;
        private readonly Mock<IRentingService> _rentingServiceMock;
        private readonly RentingsController _controller;

        public RentingsControllerTests()
        {
            _customerServiceMock = new Mock<ICustomerService>();
            _rentingServiceMock = new Mock<IRentingService>();
            _controller = new RentingsController(_rentingServiceMock.Object, _customerServiceMock.Object);
        }

        [Fact]
        public async Task Index_should_return_correct_view_with_data()
        {
            // Arrange
            int page = 1;
            var customer1 = new Customer
            {
                FirstName = "Mati",
                LastName = "Maasikas",
                PhoneNum = 57934854,
                Address = "Pärnu"
            };
            var data = new List<Renting>
            {
                new Renting { RentalNo = 1,
                RentalDate = new DateTime(2024, 11, 25),
                RentalDueTime = new DateTime(2024, 12, 25),
                DriveDistance = 23000,
                CustomerId = customer1.Id,}
            };
            var pagedResult = new PagedResult<Renting> { Results = data };
            _rentingServiceMock.Setup(x => x.List(page, It.IsAny<int>(), null)).ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.Index(page) as ViewResult;
            var model = (RentingsIndexModel)result.Model;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(pagedResult, model.Data);
        }

        [Fact]
        public async Task Details_should_return_notfound_when_id_is_missing()
        {
            // Arrange
            int? id = null;

            // Act
            var result = await _controller.Details(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Details_should_return_notfound_when_list_is_missing()
        {
            // Arrange
            int id = 1;
            var list = (Renting)null;
            _rentingServiceMock
                .Setup(x => x.Get(id))
                .ReturnsAsync(list);

            // Act
            var result = await _controller.Details(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Details_should_return_view_with_model_when_list_was_found()
        {
            // Arrange
            int id = 1;
            var list = new Renting { Id = id };
            _rentingServiceMock
                .Setup(x => x.Get(id))
                .ReturnsAsync(list);

            // Act
            var result = await _controller.Details(id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(
                string.IsNullOrEmpty(result.ViewName) ||
                result.ViewName == "Details"
            );
            Assert.Equal(list, result.Model);
        }

        [Fact]
        public async void Create_should_return_view()
        {
            // Arrange

            var customers = new List<Customer>();
            _customerServiceMock
                .Setup(x => x.Lookup())
                .ReturnsAsync(customers);

            // Act
            var result = await _controller.Create() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(
                string.IsNullOrEmpty(result.ViewName) ||
                result.ViewName == "Create"
            );
        }

        [Fact]
        public async Task Edit_should_return_notfound_when_id_is_missing()
        {
            // Arrange
            int? id = null;

            // Act
            var result = await _controller.Edit(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Edit_should_return_notfound_when_list_is_missing()
        {
            // Arrange
            int id = 1;
            var list = (Renting)null;
            _rentingServiceMock
                .Setup(x => x.Get(id))
                .ReturnsAsync(list);

            // Act
            var result = await _controller.Edit(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Edit_should_return_view_with_model_when_list_was_found()
        {
            // Arrange
            int id = 1;
            var list = new Renting { Id = id };
            var customers = new List<Customer>();
            _rentingServiceMock
                .Setup(x => x.Get(id))
                .ReturnsAsync(list);
            _customerServiceMock
                .Setup(x => x.Lookup())
                .ReturnsAsync(customers);

            // Act
            var result = await _controller.Edit(id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(
                string.IsNullOrEmpty(result.ViewName) ||
                result.ViewName == "Edit"
            );
            Assert.Equal(list, result.Model);
        }

        [Fact]
        public async Task Delete_should_return_notfound_when_id_is_missing()
        {
            // Arrange
            int? id = null;

            // Act
            var result = await _controller.Delete(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_should_return_notfound_when_list_is_missing()
        {
            // Arrange
            int id = 1;
            var list = (Renting)null;
            _rentingServiceMock
                .Setup(x => x.Get(id))
                .ReturnsAsync(list);

            // Act
            var result = await _controller.Delete(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_should_return_view_with_model_when_list_was_found()
        {
            // Arrange
            int id = 1;
            var list = new Renting { Id = id };
            _rentingServiceMock
                .Setup(x => x.Get(id))
                .ReturnsAsync(list);

            // Act
            var result = await _controller.Delete(id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(
                string.IsNullOrEmpty(result.ViewName) ||
                result.ViewName == "Delete"
            );
            Assert.Equal(list, result.Model);
        }
    }
}
