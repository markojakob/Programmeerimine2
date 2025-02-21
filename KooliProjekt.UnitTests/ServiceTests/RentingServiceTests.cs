using Castle.Core.Resource;
using KooliProjekt.Data;
using KooliProjekt.Search;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Routing;
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

        [Fact]
        public async Task List_ShouldReturnCorrectRentals_WhenKeywordIsProvided()
        {
            // Arrange: Set up initial data
            var rentals = new[]
            {
                new Renting { RentalNo = 11, RentalDate = DateTime.Now.AddDays(1), RentalDueTime = DateTime.Now.AddDays(2) },
                new Renting { RentalNo = 12, RentalDate = DateTime.Now.AddDays(2), RentalDueTime = DateTime.Now.AddDays(3) }
            };

            await DbContext.Rentings.AddRangeAsync(rentals);
            await DbContext.SaveChangesAsync();

            var search = new RentingsSearch { Keyword = "11" };

            // Act: Call the List method of your service (which you need to implement)
            var rentingService = new RentingService(DbContext); // Replace with your actual service class
            var result = await rentingService.List(1, 10, search);

            // Assert: Check if the correct rental is returned
            // Only one rental should match
            Assert.Equal(1, result);  
            Assert.Equal(123, result.rentals[0].RentalNo); // Ensure that the correct rental is returned
        }

        [Fact]
        public async Task List_ShouldReturnEmpty_WhenNoRentalsMatchKeyword()
        {
            // Arrange: Set up initial data
            var rentals = new[]
            {
                new Renting { RentalNo = 123, RentalDate = DateTime.Now.AddDays(1), RentalDueTime = DateTime.Now.AddDays(2) },
                new Renting { RentalNo = 456, RentalDate = DateTime.Now.AddDays(2), RentalDueTime = DateTime.Now.AddDays(3) }
            };

            await DbContext.Rentings.AddRangeAsync(rentals);
            await DbContext.SaveChangesAsync();

            // Arrange: Create a RentingSearch object with Keyword = "789"
            var search = new RentingsSearch { Keyword = "789" };

            // Act: Call the List method
            var rentingService = new RentingService(DbContext); // Replace with your actual service class
            var result = await rentingService.List(1, 10, search);

            // Assert: Ensure no results are returned
            Assert.AreEqual(0, result.Items.Count); // No rental should match
        }
    }
}
