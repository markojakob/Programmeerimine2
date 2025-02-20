using KooliProjekt.Data;
using KooliProjekt.IntegrationTests.Helpers;
using Microsoft.EntityFrameworkCore.Sqlite.Update.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace KooliProjekt.IntegrationTests
{
    [Collection("Sequential")]
    public class RentingsControllerTests : TestBase
    {
        private readonly HttpClient _client;
        private readonly ApplicationDbContext _context;

        public RentingsControllerTests()
        {
            _client = Factory.CreateClient();
            _context = (ApplicationDbContext)Factory.Services.GetService(typeof(ApplicationDbContext));
        }

        [Fact]
        public async Task Index_should_return_correct_response()
        {
            // Arrange

            // Act
            using var response = await _client.GetAsync("/Rentings");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Details_should_return_notfound_when_list_was_not_found()
        {
            // Arrange

            // Act
            using var response = await _client.GetAsync("/Rentings/Details/178");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Details_should_return_notfound_when_id_is_missing()
        {
            // Arrange

            // Act
            using var response = await _client.GetAsync("/Rentings/Details/");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Details_should_return_ok_when_list_was_found()
        {
            // Arrange

            var car = new Car
            {
                Model = "Mustang",
                CarMaker = "Ford",
                Price = 25000,
                Colour = "Yellow",
                Description = "Sporty muscle car",
                Category = "Coupe",
                KmTariff = 20000
            };
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
            var customer = new Customer
            {
                FirstName = "Liis",
                LastName = "Lepik",
                PhoneNum = 56892345,
                Address = "Pärnu"
            };
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            var list = new Renting
            {
                RentalNo = 6,
                RentalDate = new DateTime(2024, 11, 20),
                RentalDueTime = new DateTime(2024, 11, 27),
                DriveDistance = 23000,
                CustomerId = customer.Id,
                CarId = car.Id,

            };
            _context.Rentings.Add(list);
            _context.SaveChanges();

            // Act
            using var response = await _client.GetAsync("/Rentings/Details/" + list.Id);

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
