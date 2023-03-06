namespace Bank.Application.Interfaces
{
    using Bank.Application.Dtos;

    /// <summary>
    /// Интерфейс сервиса для работы с клиентами.
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// Регистрация клиента.
        /// </summary>
        /// <param name="customerDto"> Дто клиента. </param>
        /// <returns> Дто клиента. </returns>
        Task<CustomerDto> RegisterCustomerAsync(CustomerDto customerDto);
    }
}
