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
            var list = (Customer)null;
            _customersServiceMock
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
            var list = new Customer { Id = id };
            _customersServiceMock
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
        public void Create_should_return_view()
        {
            // Act
            var result = _controller.Create() as ViewResult;

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
            var list = (Customer)null;
            _customersServiceMock
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
            var list = new Customer { Id = id };
            _customersServiceMock
                .Setup(x => x.Get(id))
                .ReturnsAsync(list);

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
            var list = (Customer)null;
            _customersServiceMock
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
            var list = new Customer { Id = id };
            _customersServiceMock
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

        [Fact]
        public async Task Create_should_return_RedirectToAction_when_model_state_was_found()
        {
            // Arrange

            var customer = new Customer { Id = 1 };

            // Act
            var result = await _controller.Create(customer) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public async Task Create_should_return_view_when_model_state_is_not_valid()
        {
            // Arrange

            var customer = new Customer { Id = 1 };
            _controller.ModelState.AddModelError("key", "Error");
            // Act
            var result = await _controller.Create(customer) as ViewResult;

            // Assert
            Assert.NotNull(result);

        }

        [Fact]
        public async Task Edit_should_return_NotFound_when_id_is_not_customer_id()
        {
            // Arrange
            var customer = new Customer { Id = 1 };  
            int id = 2;  

            // Act
            var result = await _controller.Edit(id, customer);  

            // Assert
            Assert.NotNull(result);  
            Assert.IsType<NotFoundResult>(result);  
        }


        [Fact]
        public async Task Edit_should_return_RedirectToAction_when_Modelstate_is_valid()
        {
            // Arrange
            
            var customer = new Customer {
                Id = 1 ,
                FirstName = "Anna",
                LastName = "Kivi",
                PhoneNum = 51234567,
                Address = "Narva"
            };
            // Act
            var result = await _controller.Edit(customer.Id, customer) as RedirectToActionResult;

            // Assert

            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);

        }

        [Fact]
        public async Task Edit_should_return_view__when_model_state_is_not_valid()
        {
            int id = 2;
            var customer = new Customer
            {
                Id = id ,
                FirstName = "Anna",
                LastName = "Kivi",
                PhoneNum = 51234567,
                Address = "Narva"
            };
            _controller.ModelState.AddModelError("key", "Error");

            // Act
            var result = await _controller.Edit(id, customer) as ViewResult;


            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteConfirmed_should_delete_list()
        {
            // Arrange
            int id = 1;
            _customersServiceMock
                .Setup(x => x.Delete(id))
                .Verifiable();
            _controller.ModelState.AddModelError("key", "error");

            // Act
            var result = await _controller.DeleteConfirmed(id) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            _customersServiceMock.VerifyAll();
        }
    }
}
