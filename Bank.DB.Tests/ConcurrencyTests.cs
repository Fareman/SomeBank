namespace Bank.Tests
{
    using Bank.Domain.Entities;
    using Bank.Infrastructure;
    using Bank.Infrastructure.Repositories;
    using Microsoft.EntityFrameworkCore;

    public class ConcurrencyTests
    {
        [Fact]
        public async void ConcurrentlyEditBalanceTest()
        {
            // Arrange
            var context = GetBankContext();
            context.Database.Migrate();

            var accountIds = await CreateCustomersWithAccountsAsync(context);
            var initialBalances = await GetCurrentBalanceAsync(context, accountIds);

            // Act
            var tasks = new List<Task>();
            for (var i = 0; i < 10; i++)
            {
                tasks.Add(UpdateBalancesAsync(accountIds));
            }
            await Task.WhenAll(tasks.ToArray());

            var finalBalances = await GetCurrentBalanceAsync(context, accountIds);

            // Assert
            for (var i = 0; i < accountIds.Count; i++)
            {
                var initialBalance = initialBalances[i];
                var finalBalance = finalBalances[i];
                var expectedBalance = initialBalance + (10 * tasks.Count);
                Assert.Equal(expectedBalance, finalBalance);
            }
        }

        [Fact]
        public async Task CreateCuncurrentCustomer()
        {
            // Arrange
            var customer = new Customer
            {
                FirstName = "Name",
                MiddleName = "Middle",
                LastName = "Surname",
                BirthDate = DateTime.Now,
                Accounts = new List<Account>
                {
                    new Account
                    {
                        Balance = 0
                    }
                }
            };

            using var context = GetBankContext();
            context.Database.Migrate();
            await context.Customers.AddAsync(customer);
            await context.SaveChangesAsync();

            var accountId = customer.Accounts[0].Id;
            var initialBalance = (await context.Accounts.FirstAsync(a => a.Id == accountId)).Balance;
            const int Amount = 10;

            // Act
            var tasks = new List<Task>();
            for (var i = 0; i < 10; i++)
            {
                var threadContext = GetBankContext();
                var threadAccountRepository = new AccountRepository(threadContext);
                tasks.Add(threadAccountRepository.EditBalanceAsync(accountId, Amount));
            }
            await Task.WhenAll(tasks.ToArray());

            // Assert
            var finalContext = GetBankContext();
            var finalBalance = (await finalContext.Accounts.FirstAsync(a => a.Id == accountId)).Balance;
            Assert.Equal(initialBalance, (finalBalance - 10 * 10));
        }

        /// <summary>
        /// Получение контекста БД.
        /// </summary>
        /// <returns> Контекст БД. </returns>
        private static BankContext GetBankContext()
        {
            const string ConnectionString = $"Host=localhost;Username=postgres;Password=mysecretpassword;Port=5432;Database=BankDB;";
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            var options = new DbContextOptionsBuilder<BankContext>().UseNpgsql(ConnectionString).Options;

            return new BankContext(options);
        }

        /// <summary>
        /// Получение балансов счетов по списку Id.
        /// </summary>
        /// <param name="context"> Контекст БД. </param>
        /// <param name="accountIds"> Список Id счетов. </param>
        /// <returns> Список средств на счетах. </returns>
        private static async Task<List<decimal>> GetCurrentBalanceAsync(BankContext context, List<int> accountIds)
        {
            return await context.Accounts.Where(a => accountIds.Contains(a.Id))
                                         .Select(a => a.Balance).ToListAsync();
        }

        /// <summary>
        /// Создание тестовых клиентов.
        /// </summary>
        /// <param name="context"> Контекст БД. </param>
        /// <returns> Список Id созданных счетов. </returns>
        private static async Task<List<int>> CreateCustomersWithAccountsAsync(BankContext context)
        {
            var customers = CreateFiftyCustomers();
            await context.Customers.AddRangeAsync(customers);
            await context.SaveChangesAsync();

            return customers.SelectMany(c => c.Accounts)
                            .Select(a => a.Id).ToList();
        }

        /// <summary>
        /// Создание 50 клиентов c аккаунтами.
        /// </summary>
        /// <returns> Список клиентов. </returns>
        private static List<Customer> CreateFiftyCustomers()
        {
            var usersRange = Enumerable.Range(1, 50).ToList();
            return usersRange.Select(u => new Customer
            {
                FirstName = $"Name{u}",
                MiddleName = "Test",
                LastName = "Test",
                BirthDate = DateTime.Now,
                Accounts = new List<Account>
                {
                    new Account
                    {
                        Balance = 0
                    }
                }
            }).ToList();
        }

        /// <summary>
        /// Обновление баланса.
        /// </summary>
        /// <param name="accountIds"> Список Id счетов. </param>
        private static async Task UpdateBalancesAsync(List<int> accountIds)
        {
            var context = GetBankContext();
            const int Amount = 10;
            var accountRepository = new AccountRepository(context);
            foreach (var id in accountIds)
            {
                await accountRepository.EditBalanceAsync(id, Amount);
            }
        }
    }
}
