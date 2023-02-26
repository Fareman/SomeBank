namespace Bank.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Клиент.
    /// </summary>
    public class Customer : BaseIdEntity
    {
        /// <summary>
        /// Имя.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        /// <summary>
        /// Отчество.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string MiddleName { get; set; }

        /// <summary>
        /// Фамилия.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        /// <summary>
        /// Дата рождения.
        /// </summary>
        [Required]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Навигационное свойство.
        /// </summary>
        public List<Account> Accounts { get; set; }

    }
}
