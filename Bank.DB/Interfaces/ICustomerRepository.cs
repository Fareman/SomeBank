namespace Bank.Domain.Interfaces
{
    using Bank.Domain.Entities;

    /// <summary>
    /// Интерфейс репозитория клиентов.
    /// </summary>
    public interface ICustomerRepository
    {
        /// <summary>
        /// Добавление клиента в БД.
        /// </summary>
        /// <param name="customer"> Данные клиента. </param>
        /// <returns> Модель созданного клиента. </returns>
        Task<Customer> AddCustomerAsync(Customer customer);
    }
}
