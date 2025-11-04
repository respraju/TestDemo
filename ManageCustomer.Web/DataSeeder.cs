using ManageCustomer.Repository.Data;
using ManageCustomer.Domain.Entities;

namespace ManageCustomer.Web
{
 public static class DataSeeder
 {
 public static void Seed(CustomerDbContext db)
 {
 db.Database.EnsureCreated();
 if (!db.Customers.Any())
 {
 var customers = Enumerable.Range(1,100).Select(i => new Customer
 {
 FirstName = $"First{i}",
 LastName = $"Last{i}",
 Email = $"customer{i}@example.com",
 Phone = $"555-010{i:D3}",
 Address = $"{i} Main St, City"
 });
 db.Customers.AddRange(customers);
 }
 if (!db.Products.Any())
 {
 var products = Enumerable.Range(1,100).Select(i => new Product
 {
 ProductName = $"Product{i}",
 Category = $"Category{(i %10) +1}",
 Unit = "pcs",
 Color = (i %2 ==0) ? "Red" : "Blue"
 });
 db.Products.AddRange(products);
 }
 db.SaveChanges();

 // Seed50 orders if none exist
 if (!db.Orders.Any())
 {
 var customers = db.Customers.Take(100).ToList();
 var products = db.Products.Take(100).ToList();
 var orders = Enumerable.Range(1,50).Select(i => new Order
 {
 CustomerId = customers[i % customers.Count].Id,
 ProductId = products[i % products.Count].Id,
 OrderDate = DateTime.Today.AddDays(-i),
 TotalAmount =100 + i *10,
 Notes = $"Order note {i}"
 });
 db.Orders.AddRange(orders);
 db.SaveChanges();
 }
 }
 }
}
