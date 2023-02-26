using Bank.Domain.Entities;

namespace Bank.Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account> CreateAccountAsync(Account account);
        Task EditBalanceAsync(int accountId, decimal newBalance);
        Task<decimal?> GetAccountBalanceByIdAsync(int id);
    }
}
