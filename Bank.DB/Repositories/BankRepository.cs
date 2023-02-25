using Bank.DB.Models;

namespace Bank.DB.Repositories
{
    /// <summary>
    /// Репозиторий банка.
    /// </summary>
    public class BankRepository
    {
        /// <summary>
        /// Контекст БД.
        /// </summary>
        private readonly BankContext _context;

        /// <summary>
        /// Репозиторий банка.
        /// </summary>
        /// <param name="context">  </param>
        public BankRepository(BankContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получение баланса пользователя.
        /// </summary>
        /// <param name="id"> Идентификатор пользователя. </param>
        /// <returns> Баланс пользователя. </returns>
        public decimal GetCustomerBalanceById(int id)
        {
            return _context.Accounts.First(a => a.Id == id).Balance;
        }

        /// <summary>
        /// Регистрация клиента.
        /// </summary>
        /// <param name="customer"> Данные клиента. </param>
        /// <returns> Id клиента. </returns>
        public async Task<int> RegisterCustomerAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            _context.SaveChanges();
        }
    }
}
