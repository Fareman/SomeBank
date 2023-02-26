namespace Bank.Tests
{
    using Bank.Domain.Entities;
    using Bank.Infrastructure;
    using Bank.Infrastructure.Repositories;
    using Microsoft.EntityFrameworkCore;

    public class CustomerServiceTests
    {
        [Fact]
        public async Task CreateCustomerTest()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BankContext>()
                .UseInMemoryDatabase("Test_Database")
                .Options;

            var context = new BankContext(options);
            var customerService = new CustomerRepository(context);

            var customer = new Customer
            {
                FirstName = "Name",
                MiddleName = "MiddleName",
                LastName = "Surname",
                BirthDate = DateTime.Now,
            };

            // Act
            var createdCustomer = await customerService.AddCustomerAsync(customer);

            // Assert
            Assert.True(createdCustomer.Id > 0);
        }
    }
}
