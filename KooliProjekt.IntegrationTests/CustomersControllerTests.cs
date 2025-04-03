using KooliProjekt.Data;
using KooliProjekt.IntegrationTests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
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
    public class CustomersControllerTests : TestBase
    {
        private readonly HttpClient _client;
        private readonly ApplicationDbContext _context;

        public CustomersControllerTests()
        {
            var options = new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            };
            _client = Factory.CreateClient(options);
            _context = (ApplicationDbContext)Factory.Services.GetService(typeof(ApplicationDbContext));
        }

        [Fact]
        public async Task Index_should_return_correct_response()
        {
            // Arrange

            // Act
            using var response = await _client.GetAsync("/Customers");

            // Arrange
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Details_should_return_notfound_when_list_was_not_found()
        {
            // Arrange 

            // Act´
            using var response = await _client.GetAsync("/Customers/Details/178");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Details_should_return_notfound_when_id_is_missing()
        {
            // Arrange 

            // Act
            using var response = await _client.GetAsync("/Customers/Details/");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Details_should_return_ok_when_list_was_found()
        {
            // Arrange
            var list = new Customer
            {
                FirstName = "Liis",
                LastName = "Lepik",
                PhoneNum = 56892345,
                Address = "Pärnu"
            };
            _context.Customers.Add(list);
            _context.SaveChanges();

            // Act
            using var response = await _client.GetAsync("/Customers/Details/" + list.Id);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Create_should_save_new_list()
        {
            // Arrange
            var formValues = new Dictionary<string, string>();
            formValues.Add("FirstName", "Anna");
            formValues.Add("LastName", "Kivi");
            formValues.Add("PhoneNum", "432423");
            formValues.Add("Address", "Tallinn");

            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/Customers/Create", content);

            // Assert
            Assert.True(
                response.StatusCode == HttpStatusCode.Redirect ||
                response.StatusCode == HttpStatusCode.MovedPermanently);

            var list = _context.Customers.FirstOrDefault();
            Assert.NotNull(list);
            Assert.NotEqual(0, list.Id);
            Assert.Equal("Anna", list.FirstName);
        }

        [Fact]
        public async Task Create_should_not_save_invalid_new_list()
        {
            // Arrange
            var formValues = new Dictionary<string, string>();
            formValues.Add("FirstName", "");
            formValues.Add("LastName", "");

            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/Customers/Create", content);

            // Assert
            Assert.False(_context.Customers.Any());
        }

        [Fact]
        public async Task DeleteConfirmed_should_delete_item()
        {
            var formValues = new Dictionary<string, string>();
            formValues.Add("FirstName", "Anna");
            formValues.Add("LastName", "Kivi");
            formValues.Add("PhoneNum", "432423");
            formValues.Add("Address", "Tallinn");

            using var content = new FormUrlEncodedContent(formValues);

            using var response = await _client.PostAsync("/Customers/Delete", content);

            Assert.True(
                response.StatusCode == HttpStatusCode.Redirect ||
                response.StatusCode == HttpStatusCode.MovedPermanently);

            Assert.Empty(await response.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task Edit_should_return_notfound_when_id_is_different()
        {

            var formValues = new Dictionary<string, string>();

            formValues.Add("FirstName", "Anna");
            formValues.Add("LastName", "Kivi");
            formValues.Add("PhoneNum", "432423");
            formValues.Add("Address", "Tallinn");

            using var content = new FormUrlEncodedContent(formValues);

            using var response = await _client.PostAsync("/Customers/Edit/999", content);


            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Edit_should_not_edit_invalid_customer()
        {
            var formValues = new Dictionary<string, string>();
            formValues.Add("FirstName", "Anna");
            formValues.Add("LastName", "Kivi");

            using var content = new FormUrlEncodedContent(formValues);

            using var response = await _client.PostAsync("/Customers/Edit", content);

            Assert.False(response.IsSuccessStatusCode);


            Assert.False(_context.Cars.Any());
        }

        [Fact]
        public async Task Edit_should_return_null_when_customer_is_null()
        {
            var formValues = new Dictionary<string, string>();
            formValues.Add("Id", "900");
            formValues.Add("FirstName", "Anna");
            formValues.Add("LastName", "Kivi");
            formValues.Add("PhoneNum", "432423");
            formValues.Add("Address", "Tallinn");

            using var content = new FormUrlEncodedContent(formValues);

            using var response = await _client.PostAsync("/Customers/Edit/1  ", content);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        }
    }
}
