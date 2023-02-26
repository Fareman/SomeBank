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
        /// Репозиторий счетов.
        /// </summary>
        /// <param name="context"> Контекст БД. </param>
        public AccountRepository(BankContext context)
        {
            _context = context;
        }

        public async Task<Account> CreateAccountAsync(Account account)
        {
            await _context.Accounts.AddAsync(account);
            _context.SaveChanges();

            return account;
        }

        public async Task EditBalanceAsync(int accountId, decimal newBalance)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == accountId);
            if (account == null)
                throw new ArgumentNullException("Счет не найден.");

            account.Balance = newBalance;
            _context.SaveChanges();
        }

        ///<inheritdoc/>>
        public async Task<decimal?> GetAccountBalanceByIdAsync(int id)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == id);
            return account?.Balance;
        }
    }
}
