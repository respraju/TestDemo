using ManageCustomer.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManageCustomer.Domain.Interfaces
{
    public interface ICustomerRepository
    {
   Task<Customer> AddCustomerAsync(Customer customer);
   Task<Customer?> EditCustomerAsync(Customer customer);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer?> GetCustomerByIdAsync(int id);
   Task<IEnumerable<Customer>> SearchCustomersAsync(string searchTerm);
   Task<bool> DeleteCustomerAsync(int id);
    }
}