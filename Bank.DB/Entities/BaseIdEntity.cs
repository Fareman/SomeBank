namespace Bank.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Базовая сущность таблиц с идентификатором.
    /// </summary>
    public class BaseIdEntity
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}
