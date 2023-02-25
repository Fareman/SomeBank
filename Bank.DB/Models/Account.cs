using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bank.DB.Models
{
    /// <summary>
    /// Модель счета пользователя.
    /// </summary>
    public class Account : BaseIdEntity
    {
        /// <summary>
        /// Клиент.
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// Внешний ключ клиента.
        /// </summary>
        [ForeignKey(nameof(Customer))]
        [Required]
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Баланс пользователя.
        /// </summary>
        [Required]
        [Range(0, int.MaxValue)]
        public decimal Balance { get; set; }
    }
}
