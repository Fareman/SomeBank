namespace Bank.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Счет клиента.
    /// </summary>
    public class Account : BaseIdEntity
    {
        /// <summary>
        /// Счет пользователя.
        /// </summary>
        public Account()
        {
            Balance = 0;
        }

        /// <summary>
        /// Внешний ключ владельца счета.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Навигационное свойство для владельца счета.
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// Баланс пользователя.
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }
    }
}
