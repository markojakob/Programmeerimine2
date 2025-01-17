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

    }
}
