using ManageCustomer.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManageCustomer.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> AddProductAsync(Product product);
   Task<Product?> EditProductAsync(Product product);
   Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(int id);
  Task<bool> DeleteProductAsync(int id);
    }
}