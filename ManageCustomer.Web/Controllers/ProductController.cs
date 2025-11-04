using ManageCustomer.Domain.Entities;
using ManageCustomer.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ManageCustomer.Web.Controllers
{
 public class ProductController : Controller
 {
 private readonly IProductRepository _productRepository;

 public ProductController(IProductRepository productRepository)
 {
 _productRepository = productRepository;
 }

 public async Task<IActionResult> Index(string? searchTerm, int page =1, int pageSize =10)
 {
 IEnumerable<Product> products;
 if (!string.IsNullOrWhiteSpace(searchTerm))
 {
 products = (await _productRepository.GetAllProductsAsync())
 .Where(p => p.ProductName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
 || p.Category.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
 || p.Unit.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
 || p.Color.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
 }
 else
 {
 products = await _productRepository.GetAllProductsAsync();
 }
 ViewBag.SearchTerm = searchTerm;
 int totalItems = products.Count();
 var pagedProducts = products.Skip((page -1) * pageSize).Take(pageSize).ToList();
 ViewBag.Page = page;
 ViewBag.PageSize = pageSize;
 ViewBag.TotalItems = totalItems;
 return View(pagedProducts);
 }

 public IActionResult Create()
 {
 return View();
 }

 [HttpPost]
 [ValidateAntiForgeryToken]
 public async Task<IActionResult> Create(Product product)
 {
 if (ModelState.IsValid)
 {
 await _productRepository.AddProductAsync(product);
 return RedirectToAction(nameof(Index));
 }
 return View(product);
 }

 public async Task<IActionResult> Edit(int id)
 {
 var product = await _productRepository.GetProductByIdAsync(id);
 if (product == null) return NotFound();
 return View(product);
 }

 [HttpPost]
 [ValidateAntiForgeryToken]
 public async Task<IActionResult> Edit(Product product)
 {
 if (ModelState.IsValid)
 {
 await _productRepository.EditProductAsync(product);
 return RedirectToAction(nameof(Index));
 }
 return View(product);
 }

 public async Task<IActionResult> Delete(int id)
 {
 var product = await _productRepository.GetProductByIdAsync(id);
 if (product == null) return NotFound();
 return View(product);
 }

 [HttpPost, ActionName("Delete")]
 [ValidateAntiForgeryToken]
 public async Task<IActionResult> DeleteConfirmed(int id)
 {
 await _productRepository.DeleteProductAsync(id);
 return RedirectToAction(nameof(Index));
 }
 }
}
