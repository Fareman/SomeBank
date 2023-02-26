
namespace Bank.Application.Services
{
    using Bank.Application.Dtos;
    using Bank.Application.Interfaces;
    using Bank.Domain.Entities;
    using Bank.Domain.Interfaces;

    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        /// <summary>
        /// Регистрация клиента.
        /// </summary>
        /// <param name="customerDto"> Дто клиента. </param>
        /// <returns> Дто клиента с данными из БД. </returns>
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
