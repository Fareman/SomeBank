using Bank.Application.Dtos;

namespace Bank.Application.Interfaces
{
    public interface IAccountService
    {
        Task ChangeBalance(int accountId, decimal newBalance);
        Task<AccountDto> CreateAccount(AccountDto accountDto);
        Task<decimal?> GetAccountBalanceAsync(int accountId);
    }
}
