namespace Bank.DB.Models
{
    /// <summary>
    /// Модель пользователя.
    /// </summary>
    public class Customer : BaseIdEntity
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия пользователя.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Дата рождения.
        /// </summary>
        public DateTime BirthDate { get; set; }

    }
}
