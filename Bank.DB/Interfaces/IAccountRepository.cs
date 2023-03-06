using Bank.Domain.Entities;

namespace Bank.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория счетов.
    /// </summary>
    public interface IAccountRepository
    {
        /// <summary>
        /// Создание счета.
        /// </summary>
        /// <param name="account"> Модель счета. </param>
        /// <returns> Модель счета. </returns>
        Task<Account> CreateAccountAsync(Account account);

        /// <summary>
        /// Изменение баланса счета.
        /// </summary>
        /// <param name="accountId"> Id счета. </param>
        /// <param name="amount"> Кол-во начисленных/списанных единиц. </param>
        Task EditBalanceAsync(int accountId, decimal amount);

        /// <summary>
        /// Получение баланса счета.
        /// </summary>
        /// <param name="accountId"> Id счета. </param>
        /// <returns> Баланс. </returns>
        Task<decimal> GetAccountBalanceByIdAsync(int accountId);
    }
}
