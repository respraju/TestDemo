using ManageCustomer.Domain.Entities;
using ManageCustomer.Domain.Interfaces;
using ManageCustomer.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace ManageCustomer.Repository
{
 public class OrderRepository : IOrderRepository
 {
 private readonly CustomerDbContext _context;

 public OrderRepository(CustomerDbContext context)
 {
 _context = context;
 }

 public async Task<Order> AddOrderAsync(Order order)
 {
 _context.Orders.Add(order);
 await _context.SaveChangesAsync();
 return order;
 }

 public async Task<Order?> EditOrderAsync(Order order)
 {
 var existing = await _context.Orders.FindAsync(order.Id);
 if (existing == null) return null;
 _context.Entry(existing).CurrentValues.SetValues(order);
 await _context.SaveChangesAsync();
 return existing;
 }

 public async Task<IEnumerable<Order>> GetAllOrdersAsync()
 {
 return await _context.Orders.Include(o => o.Customer).ToListAsync();
 }

 public async Task<Order?> GetOrderByIdAsync(int id)
 {
 return await _context.Orders.Include(o => o.Customer).FirstOrDefaultAsync(o => o.Id == id);
 }

 public async Task<bool> DeleteOrderAsync(int id)
 {
 var order = await _context.Orders.FindAsync(id);
 if (order == null) return false;
 _context.Orders.Remove(order);
 await _context.SaveChangesAsync();
 return true;
 }
 }
}
