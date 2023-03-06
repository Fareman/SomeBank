using Bank.Application.Dtos;

namespace Bank.Application.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса для работы со счетами.
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Изменение баланса счета.
        /// </summary>
        /// <param name="accountId"> Id счета. </param>
        /// <param name="amount"> Кол-во списанных/начисленных единиц. </param>
        Task ChangeBalance(int accountId, decimal amount);

        /// <summary>
        /// Регистация счета.
        /// </summary>
        /// <param name="accountDto"> Дто счета. </param>
        /// <returns> Дто счета. </returns>
        Task<AccountDto> CreateAccount(AccountDto accountDto);

        /// <summary>
        /// Получение баланса на счете.
        /// </summary>
        /// <param name="accountId"> Id счета. </param>
        /// <returns> Баланс. </returns>
        Task<decimal> GetAccountBalanceAsync(int accountId);
    }
}
