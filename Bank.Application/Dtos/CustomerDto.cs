namespace Bank.Application.Dtos
{
    /// <summary>
    /// Дто клиента.
    /// </summary>
    public class CustomerDto
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Отчетство.
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Фамилия.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Дата рождения.
        /// </summary>
        public DateTimeOffset BirthDate { get; set; }
    }
}
