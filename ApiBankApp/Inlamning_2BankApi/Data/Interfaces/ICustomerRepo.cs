using ApiBankApp.Data.Entities;

namespace ApiBankApp.Data.Interfaces
{
    public interface ICustomerRepo
    {
        Task<Customer?> GetCustomerByIdAsync(int id);

        Task<Customer> AddCustomerAsync(Customer customer);
    }
}
