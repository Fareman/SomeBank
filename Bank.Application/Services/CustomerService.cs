
namespace Bank.Application.Services
{
    using Bank.Application.Dtos;
    using Bank.Application.Interfaces;
    using Bank.Domain.Entities;
    using Bank.Domain.Interfaces;

    /// <summary>
    /// Сервис для работы с клиентами.
    /// </summary>
    public class CustomerService : ICustomerService
    {
        /// <summary>
        /// Репозиторий клиентов.
        /// </summary>
        private readonly ICustomerRepository _customerRepository;

        /// <summary>
        /// Сервис для работы с клиентами.
        /// </summary>
        /// <param name="customerRepository"> Репозиторий клиентов. </param>
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        /// <inheritdoc />
        public async Task<CustomerDto> RegisterCustomerAsync(CustomerDto customerDto)
        {
            var customer = new Customer
            {
                FirstName = customerDto.FirstName,
                MiddleName = customerDto.MiddleName,
                LastName = customerDto.LastName,
                BirthDate = customerDto.BirthDate
            };

            var createdCustomer = await _customerRepository.AddCustomerAsync(customer);

            return new CustomerDto
            {
                FirstName = createdCustomer.FirstName,
                MiddleName = createdCustomer.MiddleName,
                LastName = createdCustomer.LastName,
                BirthDate = createdCustomer.BirthDate
            };
        }
    }
}
