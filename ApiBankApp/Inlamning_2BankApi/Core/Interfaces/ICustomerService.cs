using ApiBankApp.Data.Entities;

namespace ApiBankApp.Core.Interfaces
{
    public interface ICustomerService
    {
        Task<Customer?> GetCustomerByIdAsync(int id);
        Task<Customer> CreateCustomerAsync(Customer customer);
    }
}
