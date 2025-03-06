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
    public class CustomerServiceTests : ServiceTestBase
    {
        [Fact]
        public async Task Save_should_add_new_list()
        {
            // Arrange
            var service = new CustomerService(DbContext);
            var customer = new Customer
            {
                FirstName = "Mati",
                LastName = "Maasikas",
                PhoneNum = 57934854,
                Address = "Pärnu"
            };

            // Act
            await service.Save(customer);

            // Assert
            var count = DbContext.Customers.Count();
            var result = DbContext.Customers.FirstOrDefault();
            Assert.NotNull(result);
            Assert.Equal(1, count);
            Assert.Equal(customer.Id, result.Id);
        }

        [Fact]
        public async Task Save_should_update_old_list()
        {

            // Arrange
            var service = new CustomerService(DbContext);
            var customer = new Customer
            {
                FirstName = "Mati",
                LastName = "Maasikas",
                PhoneNum = 57934854,
                Address = "Pärnu"
            };

            DbContext.Customers.Add(customer);
            DbContext.SaveChanges();

            // Act
            customer.PhoneNum = 58787322;

            await service.Save(customer);

            // Assert
            var updatedCustomer = await DbContext.Customers.FindAsync(1);
            Assert.Equal(customer.PhoneNum, updatedCustomer.PhoneNum);

        }

        [Fact]
        public async Task Delete_should_remove_given_list()
        {
            // Arrange
            var service = new CustomerService(DbContext);
            var customer = new Customer
            {
                Id = 1,
                FirstName = "Mati",
                LastName = "Maasikas",
                PhoneNum = 57934854,
                Address = "Pärnu"
            };
            DbContext.Customers.Add(customer);
            DbContext.SaveChanges();

            // Act
            await service.Delete(1);

            // Assert
            var count = DbContext.Customers.Count();
            Assert.Equal(0, count);
        }

        [Fact]
        public async Task Get_should_return_customer_by_id()
        {
            // Arrange
            var service = new CustomerService(DbContext);
            var customer = new Customer
            {
                FirstName = "Mati",
                LastName = "Maasikas",
                PhoneNum = 57934854,
                Address = "Pärnu"
            };

            DbContext.Customers.Add(customer);
            DbContext.SaveChanges();

            // Act
            var result = await service.Get(1);

            // Arrange
            Assert.Equal(customer.Id, result.Id);

        }
    }
}