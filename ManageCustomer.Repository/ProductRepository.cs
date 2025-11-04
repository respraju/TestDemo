using ManageCustomer.Domain.Entities;
using ManageCustomer.Domain.Interfaces;
using ManageCustomer.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace ManageCustomer.Repository
{
 public class ProductRepository : IProductRepository
 {
 private readonly CustomerDbContext _context;

 public ProductRepository(CustomerDbContext context)
 {
 _context = context;
 }

 public async Task<Product> AddProductAsync(Product product)
 {
 _context.Products.Add(product);
 await _context.SaveChangesAsync();
 return product;
 }

 public async Task<Product?> EditProductAsync(Product product)
 {
 var existing = await _context.Products.FindAsync(product.Id);
 if (existing == null) return null;
 _context.Entry(existing).CurrentValues.SetValues(product);
 await _context.SaveChangesAsync();
 return existing;
 }

 public async Task<IEnumerable<Product>> GetAllProductsAsync()
 {
 return await _context.Products.ToListAsync();
 }

 public async Task<Product?> GetProductByIdAsync(int id)
 {
 return await _context.Products.FindAsync(id);
 }

 public async Task<bool> DeleteProductAsync(int id)
 {
 var product = await _context.Products.FindAsync(id);
 if (product == null) return false;
 _context.Products.Remove(product);
 await _context.SaveChangesAsync();
 return true;
 }
 }
}
