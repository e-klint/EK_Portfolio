using ApiBankApp.Core.Interfaces;
using ApiBankApp.Data.Entities;
using ApiBankApp.Data.Interfaces;

namespace ApiBankApp.Core.Services
{
    public class CustomerService: ICustomerService
    {
        private readonly ICustomerRepo _customerRepo;

        public CustomerService(ICustomerRepo customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            return await _customerRepo.GetCustomerByIdAsync(id);
        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            return await _customerRepo.AddCustomerAsync(customer);
        }
    }
}
