namespace Bank.Infrastructure.Repositories
{
    using Bank.Domain.Entities;
    using Bank.Domain.Interfaces;
    using Bank.Infrastructure;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Репозиторий счетов.
    /// </summary>
    public class AccountRepository : IAccountRepository
    {
        /// <summary>
        /// Контекст БД.
        /// </summary>
        private readonly BankContext _context;

        /// <summary>
        /// Блокировщик.
        /// </summary>
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        /// <summary>
        /// Репозиторий счетов.
        /// </summary>
        /// <param name="context"> Контекст БД. </param>
        public AccountRepository(BankContext context)
        {
            _context = context;
        }

        ///<inheritdoc/>>
        public async Task<Account> CreateAccountAsync(Account account)
        {
            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();

            return account;
        }

        ///<inheritdoc/>>
        public async Task EditBalanceAsync(int accountId, decimal amount)
        {
            await _semaphore.WaitAsync();

            try
            {
                var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == accountId);
                if (account == null)
                    throw new ArgumentNullException("Счет не найден.");

                account.Balance += amount;
                _context.SaveChanges();
                _semaphore.Release();
            }
            catch
            {
                _semaphore.Release();
            }
        }

        ///<inheritdoc/>>
        public async Task<decimal> GetAccountBalanceByIdAsync(int accountId)
        {
            var account = await _context.Accounts.AsNoTracking()
                                                 .FirstAsync(a => a.Id == accountId);
            return account.Balance;
        }
    }
}
