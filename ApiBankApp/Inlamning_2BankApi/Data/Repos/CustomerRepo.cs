using ApiBankApp.Data.Entities;
using ApiBankApp.Data.Interfaces;

namespace ApiBankApp.Data.Repos
{
    public class CustomerRepo: ICustomerRepo
    {
        private readonly BankAppDataContext _context;

        public CustomerRepo(BankAppDataContext context)
        {
            _context = context;
        }

        public async Task<Customer?> GetCustomerByIdAsync(int id) 
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<Customer> AddCustomerAsync(Customer customer)
        {
            await  _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync(); //Behöver denna tas bort?
            return customer;
        }
    }
}
