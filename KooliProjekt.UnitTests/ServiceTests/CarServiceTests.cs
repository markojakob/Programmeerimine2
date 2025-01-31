using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KooliProjekt.Data;
using KooliProjekt.Services;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class TodoListServiceTests : ServiceTestBase
    {
        [Fact]
        public async Task Save_should_add_new_list()
        {
            // Arrange
            var service = new CarService(DbContext);
            var car = new Car
            {
                Model = "A4",
                CarMaker = "Audi",
                Price = 5000,
                Colour = "Green",
                Description = "Fast",
                Category = "Sedan",
                KmTariff = 40000
            };

            // Act
            await service.Save(car);

            // Assert
            var count = DbContext.Cars.Count();
            var result = DbContext.Cars.FirstOrDefault();
            Assert.Equal(1, count);
            Assert.Equal(car.CarMaker, result.CarMaker);
        }

        [Fact]
        public async Task Save_should_update_old_list()
        {

            // Arrange
            var service = new CarService(DbContext);
            var car = new Car
            {
                Id = 1,
                Model = "A4",
                CarMaker = "Audi",
                Price = 5000,
                Colour = "Green",
                Description = "Fast",
                Category = "Sedan",
                KmTariff = 40000
            };
            DbContext.Cars.Add(car);
            DbContext.SaveChanges();


            car.Model = "New Model";
            car.CarMaker = "New Make";
            // Act
            await service.Save(car);

            // Assert
            var updatedCar = await DbContext.Cars.FindAsync(1);
            Assert.Equal(car.CarMaker, updatedCar.CarMaker);

        }

        [Fact]
        public async Task Delete_should_remove_given_list()
        {
            // Arrange
            var service = new CarService(DbContext);
            var car = new Car
            {
                Id = 1,
                Model = "A4",
                CarMaker = "Audi",
                Price = 5000,
                Colour = "Green",
                Description = "Fast",
                Category = "Sedan",
                KmTariff = 40000
            };
            DbContext.Cars.Add(car);
            DbContext.SaveChanges();

            // Act
            await service.Delete(1);

            // Assert
            var count = DbContext.Cars.Count();
            Assert.Equal(0, count);
        }
    }
}