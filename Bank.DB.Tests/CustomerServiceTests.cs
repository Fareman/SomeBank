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
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            var options = new DbContextOptionsBuilder<BankContext>()
                .UseNpgsql("Host=localhost;Username=postgres;Password=mysecretpassword;Port=5432;Database=BankDB;")
                .Options;

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
        public async void ConcurrentlyEditBalanceTest()
        {
            // Arrange
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            var options = new DbContextOptionsBuilder<BankContext>()
                .UseNpgsql("Host=localhost;Username=postgres;Password=mysecretpassword;Port=5432;Database=BankDB;")
                .Options;

            var context = new BankContext(options);
            context.Database.Migrate();
            var users = Enumerable.Range(1, 1).ToList();
            var customerIds = await CreateCustomers(context, users);
            var accountIds = await CreateAccountsAsync(context, customerIds);

            var initialBalances = await GetCurrentBalanceAsync(options, accountIds);

            // Act
            var tasks = new List<Task>();
            for (var i = 0; i < 10; i++)
            {
                tasks.Add(Task.Run(() => UpdateBalances(accountIds, options)));
            }
            Task.WaitAll(tasks.ToArray());

            var finalBalances = await GetCurrentBalanceAsync(options, accountIds);

            // Assert
            for (var i = 0; i < accountIds.Count; i++)
            {
                var id = accountIds[i];
                var initialBalance = initialBalances[i];
                var finalBalance = finalBalances[i];
                var expectedBalance = initialBalance + (10 * tasks.Count);
                Assert.Equal(expectedBalance, finalBalance);
            }
        }

        private static async Task<List<decimal>> GetCurrentBalanceAsync(DbContextOptions<BankContext> options, List<int> accountIds)
        {
            var context = new BankContext(options);
            var accountRepository = new AccountRepository(context);
            var tasks = accountIds.Select(async id => await accountRepository.GetAccountBalanceByIdAsync(id));
            return (await Task.WhenAll(tasks)).ToList();
        }

        private static async Task<List<int>> CreateAccountsAsync(BankContext context, List<int> customerIds)
        {
            var newAccounts = customerIds.Select(id => new Account
            {
                CustomerId = id,
                Balance = 0
            }).ToList();

            var accountRepository = new AccountRepository(context);
            var tasks = newAccounts.Select(async a => await accountRepository.CreateAccountAsync(a));
            var dbAccounts = await Task.WhenAll(tasks);

            return dbAccounts.Select(account => account.Id).ToList();
        }

        private static async Task<List<int>> CreateCustomers(BankContext context, List<int> users)
        {
            var customers = users.Select(u => new Customer
            {
                FirstName = $"Name{u}",
                MiddleName = "Test",
                LastName = "Test",
                BirthDate = DateTime.Now,
            }).ToList();

            var customerRepository = new CustomerRepository(context);
            var tasks = customers.Select(async c => await customerRepository.AddCustomerAsync(c));
            var dbCustomers = await Task.WhenAll(tasks);

            return dbCustomers.Select(c => c.Id).ToList();
        }

        private async void UpdateBalances(List<int> accountIds, DbContextOptions<BankContext> options)
        {
            var context = new BankContext(options);
            var accountRepository = new AccountRepository(context);

            for (var i = 0; i < accountIds.Count; i++)
            {
                var id = accountIds[i];
                var amount = 10;
                await accountRepository.EditBalanceAsync(id, amount);
            }
        }
    }
}
