using Bank.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace Bank.DB
{
    /// <summary>
    /// Контекст работы с БД.
    /// </summary>
    public class BankContext : DbContext
    {
        /// <summary>
        /// Контекст работы с БД.
        /// </summary>
        /// <param name="options"> Параметры контекста. </param>
        public BankContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreatedAsync();
        }

        /// <summary>
        /// Счета.
        /// </summary>
        public DbSet<Account> Accounts { get; set; }

        /// <summary>
        /// Клиенты.
        /// </summary>
        public DbSet<Customer> Customers { get; set; }
    }
}