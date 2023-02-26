
using Bank.Application.Dtos;
using Bank.Application.Interfaces;
using Bank.Domain.Entities;
using Bank.Domain.Interfaces;

namespace Bank.Application.Services
{
    public class AccountService : IAccountService
    {
        private IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task ChangeBalance(int accountId, decimal newBalance)
        {
            await _accountRepository.EditBalanceAsync(accountId, newBalance);
        }

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

        public async Task<decimal?> GetAccountBalanceAsync(int accountId)
        {
            return await _accountRepository.GetAccountBalanceByIdAsync(accountId);
        }
    }
}
