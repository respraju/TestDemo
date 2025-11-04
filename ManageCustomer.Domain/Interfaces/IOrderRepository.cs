using ManageCustomer.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManageCustomer.Domain.Interfaces
{
 public interface IOrderRepository
 {
 Task<Order> AddOrderAsync(Order order);
 Task<Order?> EditOrderAsync(Order order);
 Task<IEnumerable<Order>> GetAllOrdersAsync();
 Task<Order?> GetOrderByIdAsync(int id);
 Task<bool> DeleteOrderAsync(int id);
 }
}
