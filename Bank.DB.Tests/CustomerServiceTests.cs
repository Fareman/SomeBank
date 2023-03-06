namespace Bank.Tests
{
    using Bank.Domain.Entities;
    using Bank.Infrastructure;
    using Bank.Infrastructure.Repositories;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;

    public class CustomerServiceTests
    {
        [Fact]
        public async Task CreateCustomerTest()
        {
            // Arrange
            var options = GetDbContextOptions();

            var context = new BankContext(options);
            context.Database.Migrate();
            var customer = new Customer
            {
                FirstName = "Name",
                MiddleName = "MiddleName",
                LastName = "Surname",
                BirthDate = DateTime.Now,
            };
            var customerService = new CustomerRepository(context);

            // Act
            var createdCustomer = await customerService.AddCustomerAsync(customer);

            // Assert
            Assert.True(context.Customers.Any(c => c.Id == createdCustomer.Id));
        }

        [Fact]
        public async Task AddCustomerAsync()
        {
            // Arrange
            var options = GetDbContextOptions();
            var context = new BankContext(options);
            var customer = new Customer
            {
                FirstName = $"Name",
                MiddleName = "Middle",
                LastName = "Surname",
                BirthDate = DateTime.Now,
                Accounts = new List<Account> {
                    new Account
                    {
                        Balance = 0
                    }
                }
            };

            // Act
            await context.AddAsync(customer);
            await context.SaveChangesAsync();

            // Assert
            Assert.True(customer.Id != 0);
        }

        private static DbContextOptions<BankContext> GetDbContextOptions()
        {
            const string ConnectionString = "Host=localhost;Username=postgres;Password=mysecretpassword;Port=5432;Database=BankDB;";
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            return new DbContextOptionsBuilder<BankContext>().
                   UseNpgsql(ConnectionString).Options;
        }
    }
}
