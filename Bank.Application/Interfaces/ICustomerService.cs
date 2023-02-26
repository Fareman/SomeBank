namespace Bank.Application.Interfaces
{
    using Bank.Application.Dtos;

    public interface ICustomerService
    {
        Task<CustomerDto> RegisterCustomerAsync(CustomerDto customerDto);
    }
}
