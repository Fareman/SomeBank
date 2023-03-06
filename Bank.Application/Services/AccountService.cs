namespace Bank.Application.Services
{
    using Bank.Application.Dtos;
    using Bank.Application.Interfaces;
    using Bank.Domain.Entities;
    using Bank.Domain.Interfaces;

    /// <summary>
    /// Сервис для работы со счетами.
    /// </summary>
    public class AccountService : IAccountService
    {
        /// <summary>
        /// Репозиторий счетов.
        /// </summary>
        private readonly IAccountRepository _accountRepository;

        /// <summary>
        /// Сервис для работы со счетами.
        /// </summary>
        /// <param name="accountRepository"> Репозиторий счетов. </param>
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        /// <inheritdoc />
        public async Task ChangeBalance(int accountId, decimal amount)
        {
            await _accountRepository.EditBalanceAsync(accountId, amount);
        }

        /// <inheritdoc />
        public async Task<AccountDto> CreateAccount(AccountDto accountDto)
        {
            var account = new Account
            {
                CustomerId = accountDto.CustomerId
            };

            var createdAccount = await _accountRepository.CreateAccountAsync(account);

            return new AccountDto
            {
                Id = createdAccount.Id,
                CustomerId = createdAccount.CustomerId
            };
        }

        /// <inheritdoc />
        public async Task<decimal> GetAccountBalanceAsync(int accountId)
        {
            return await _accountRepository.GetAccountBalanceByIdAsync(accountId);
        }
    }
}
