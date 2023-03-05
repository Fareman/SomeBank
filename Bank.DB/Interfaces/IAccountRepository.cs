using Bank.Domain.Entities;

namespace Bank.Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account> CreateAccountAsync(Account account);

        /// <summary>
        /// Изменение баланса счета.
        /// </summary>
        /// <param name="accountId"> Id счета. </param>
        /// <param name="amount"> Кол-во начисленных/списанных единиц. </param>
        Task EditBalanceAsync(int accountId, decimal amount);
        Task<decimal> GetAccountBalanceByIdAsync(int id);
    }
}
