namespace Bank.Infrastructure
{
    using Bank.Domain.Entities;
    using Microsoft.EntityFrameworkCore;

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
        }

        /// <summary>
        /// Счета.
        /// </summary>
        public DbSet<Account> Accounts { get; set; }

        /// <summary>
        /// Клиенты.
        /// </summary>
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasOne(a => a.Customer)
                .WithMany(c => c.Accounts)
                .HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}