using Castle.Core.Resource;
using KooliProjekt.Data;
using KooliProjekt.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class RentingServiceTests : ServiceTestBase
    {

        [Fact]
        public async Task Save_should_add_new_list()
        {
            // Arrange
            var service = new RentingService(DbContext);
            var car = new Car
            {
                Id = 1,
            };
            var customer = new Customer
            {
                Id = 1
            };
            var renting = new Renting
            {
                RentalNo = 1,
                RentalDate = new DateTime(2024, 11, 25),
                RentalDueTime = new DateTime(2024, 12, 25),
                DriveDistance = 23000,
                CustomerId = customer.Id,
                CarId = car.Id,
            };

            // Act
            await service.Save(renting);

            // Assert
            var count = DbContext.Rentings.Count();
            var result = DbContext.Rentings.FirstOrDefault();
            Assert.Equal(1, count);
            Assert.Equal(renting.Id, result.Id);
        }

        [Fact]
        public async Task Save_should_update_old_list()
        {

            // Arrange
            var service = new RentingService(DbContext);
            var car = new Car
            {
                Id = 1,
            };
            var customer = new Customer
            {
                Id = 1
            };
            var renting = new Renting
            {
                Id = 1,
                RentalNo = 1,
                RentalDate = new DateTime(2024, 11, 25),
                RentalDueTime = new DateTime(2024, 12, 25),
                DriveDistance = 23000,
                CustomerId = customer.Id,
                CarId = car.Id,
            };
            DbContext.Rentings.Add(renting);
            DbContext.SaveChanges();

            renting.DriveDistance = 24000;
            renting.CustomerId = 7;
            // Act
            await service.Save(renting);

            // Assert
            var updatedRenting = await DbContext.Rentings.FindAsync(1);
            Assert.Equal(renting.DriveDistance, updatedRenting.DriveDistance);
                

        }

        [Fact]
        public async Task Delete_should_remove_given_list()
        {
            // Arrange
            var service = new RentingService(DbContext);
            var car = new Car
            {
                Id = 1,
            };
            var customer = new Customer
            {
                Id = 1
            };
            var renting = new Renting
            {
                Id = 1,
                RentalNo = 1,
                RentalDate = new DateTime(2024, 11, 25),
                RentalDueTime = new DateTime(2024, 12, 25),
                DriveDistance = 23000,
                CustomerId = customer.Id,
                CarId = car.Id,
            };
            DbContext.Rentings.Add(renting);
            DbContext.SaveChanges();

            // Act
            await service.Delete(1);

            // Assert
            var count = DbContext.Rentings.Count();
            Assert.Equal(0, count);
        }

        [Fact]
        public async Task Get_should_return_renting_by_id()
        {
            // Arrange
            var service = new RentingService(DbContext);
            var car = new Car
            {
                Id = 1,
            };
            var customer = new Customer
            {
                Id = 1
            };
            var renting = new Renting
            {
                Id = 1,
                RentalNo = 1,
                RentalDate = new DateTime(2024, 11, 25),
                RentalDueTime = new DateTime(2024, 12, 25),
                DriveDistance = 23000,
                CustomerId = customer.Id,
                CarId = car.Id,
            };

            DbContext.Rentings.Add(renting);
            DbContext.SaveChanges();

            // Act
            var result = await service.Get(1);

            // Arrange
            Assert.Equal(car.Id, result.Id);

        }


    }
}
