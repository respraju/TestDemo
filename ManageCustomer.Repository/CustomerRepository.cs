using ManageCustomer.Domain.Entities;
using ManageCustomer.Domain.Interfaces;
using ManageCustomer.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace ManageCustomer.Repository
{
 public class CustomerRepository : ICustomerRepository
 {
 private readonly CustomerDbContext _context;

 public CustomerRepository(CustomerDbContext context)
 {
 _context = context;
 }

 public async Task<Customer> AddCustomerAsync(Customer customer)
 {
 _context.Customers.Add(customer);
 await _context.SaveChangesAsync();
 return customer;
 }

 public async Task<Customer?> EditCustomerAsync(Customer customer)
 {
 var existing = await _context.Customers.FindAsync(customer.Id);
 if (existing == null) return null;
 _context.Entry(existing).CurrentValues.SetValues(customer);
 await _context.SaveChangesAsync();
 return existing;
 }

 public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
 {
 return await _context.Customers.ToListAsync();
 }

 public async Task<Customer?> GetCustomerByIdAsync(int id)
 {
 return await _context.Customers.FindAsync(id);
 }

 public async Task<IEnumerable<Customer>> SearchCustomersAsync(string searchTerm)
 {
 return await _context.Customers
 .Where(c => c.FirstName.Contains(searchTerm) || c.LastName.Contains(searchTerm) || c.Email.Contains(searchTerm))
 .ToListAsync();
 }

 public async Task<bool> DeleteCustomerAsync(int id)
 {
 var customer = await _context.Customers.FindAsync(id);
 if (customer == null) return false;
 _context.Customers.Remove(customer);
 await _context.SaveChangesAsync();
 return true;
 }
 }
}