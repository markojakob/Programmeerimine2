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
    public class CarsControllerTests
    {
        private readonly Mock<ICarService> _carServiceMock;
        private readonly CarsController _controller;

        public CarsControllerTests()
        {
            _carServiceMock = new Mock<ICarService>();
            _controller = new CarsController(_carServiceMock.Object);
        }

        [Fact]
        public async Task Index_should_return_correct_view_with_data()
        {
            // Arrange
            int page = 1;
            var data = new List<Car>
            {
                new Car { Model = "Civic",
                CarMaker = "Honda",
                Price = 6000,
                Colour = "Blue",
                Description = "Reliable and economical",
                Category = "Hatchback",
                KmTariff = 25000 }
            };
            var pagedResult = new PagedResult<Car> { Results = data };
            _carServiceMock.Setup(x => x.List(page, It.IsAny<int>(), null)).ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.Index(page) as ViewResult;
            var model = (CarsIndexModel)result.Model;

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
            var list = (Car)null;
            _carServiceMock
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
            var list = new Car { Id = id };
            _carServiceMock
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
            var list = (Car)null;
            _carServiceMock
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
            var list = new Car { Id = id };
            _carServiceMock
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
            var list = (Car)null;
            _carServiceMock
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
            var list = new Car { Id = id };
            _carServiceMock
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

            var car = new Car { Id = 1 };

            // Act
            var result = await _controller.Create(car) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public async Task Create_should_return_view_when_model_state_is_not_valid()
        {
            // Arrange

            var car = new Car { Id = 1 };
            _controller.ModelState.AddModelError("key", "Error");
            // Act
            var result = await _controller.Create(car) as ViewResult;

            // Assert
            Assert.NotNull(result);

        }

        [Fact]
        public async Task Edit_should_return_NotFound_when_id_is_not_car_id()
        {
            // Arrange
            var car = new Car { Id = 1, Model = "Tesla", Price = 50000 }; 
            int id = 2;  

            // Act
            var result = await _controller.Edit(id, car); 

            // Assert
            Assert.NotNull(result); 
            Assert.IsType<NotFoundResult>(result);  
        }

        [Fact]
        public async Task Edit_should_return_RedirectToAction_when_Modelstate_is_valid()
        {
            // Arrange
            var car = new Car { Id = 1 };
            // Act
            var result = await _controller.Edit(car.Id, car) as RedirectToActionResult;

            // Assert

            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);

        }

        [Fact]
        public async Task Edit_should_return_view__when_model_state_is_not_valid()
        {
            int id = 2;
            var car = new Car {
                Id = id,
                Model = "Model 3",
                CarMaker = "Tesla",
                Price = 35000,
                Colour = "Black",
                Description = "Electric car with high performance",
                Category = "Sedan",
                KmTariff = 15000
            };
            _controller.ModelState.AddModelError("key", "Error");

            // Act
            var result = await _controller.Edit(id, car) as ViewResult;
            

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteConfirmed_should_delete_list()
        {
            // Arrange
            int id = 1;
            _carServiceMock
                .Setup(x => x.Delete(id))
                .Verifiable();
            _controller.ModelState.AddModelError("key", "error");

            // Act
            var result = await _controller.DeleteConfirmed(id) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            _carServiceMock.VerifyAll();
        }


    }
}
