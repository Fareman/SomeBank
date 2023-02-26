namespace Bank.Infrastructure.Repositories
{
    using Bank.Domain.Entities;
    using Bank.Domain.Interfaces;
    using Bank.Infrastructure;

    /// <summary>
    /// Репозиторий клиентов.
    /// </summary>
    public class CustomerRepository : ICustomerRepository
    {
        /// <summary>
        /// Контекст БД.
        /// </summary>
        private readonly BankContext _context;

        /// <summary>
        /// Репозиторий клиентов.
        /// </summary>
        /// <param name="context"> Контекст БД </param>
        public CustomerRepository(BankContext context)
        {
            _context = context;
        }

        ///<inheritdoc/>>
        public async Task<Customer> AddCustomerAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            _context.SaveChanges();

            return customer;
        }
    }
}
