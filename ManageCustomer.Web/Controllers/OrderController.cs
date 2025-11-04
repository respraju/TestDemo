using ManageCustomer.Domain.Entities;
using ManageCustomer.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ManageCustomer.Web.Controllers
{
 public class OrderController : Controller
 {
 private readonly IOrderRepository _orderRepository;
 private readonly ICustomerRepository _customerRepository;
 private readonly IProductRepository _productRepository;

 public OrderController(IOrderRepository orderRepository, ICustomerRepository customerRepository, IProductRepository productRepository)
 {
 _orderRepository = orderRepository;
 _customerRepository = customerRepository;
 _productRepository = productRepository;
 }

 public async Task<IActionResult> Index()
 {
 var orders = await _orderRepository.GetAllOrdersAsync();
 return View(orders);
 }

 public async Task<IActionResult> Create()
 {
 ViewBag.Customers = new SelectList(await _customerRepository.GetAllCustomersAsync(), "Id", "FirstName");
 ViewBag.Products = new SelectList(await _productRepository.GetAllProductsAsync(), "Id", "ProductName");
 return View();
 }

 [HttpPost]
 [ValidateAntiForgeryToken]
 public async Task<IActionResult> Create(Order order)
 {
 if (ModelState.IsValid)
 {
 await _orderRepository.AddOrderAsync(order);
 return RedirectToAction(nameof(Index));
 }
 ViewBag.Customers = new SelectList(await _customerRepository.GetAllCustomersAsync(), "Id", "FirstName", order.CustomerId);
 ViewBag.Products = new SelectList(await _productRepository.GetAllProductsAsync(), "Id", "ProductName", order.ProductId);
 return View(order);
 }

 public async Task<IActionResult> Edit(int id)
 {
 var order = await _orderRepository.GetOrderByIdAsync(id);
 if (order == null) return NotFound();
 ViewBag.Customers = new SelectList(await _customerRepository.GetAllCustomersAsync(), "Id", "FirstName", order.CustomerId);
 ViewBag.Products = new SelectList(await _productRepository.GetAllProductsAsync(), "Id", "ProductName", order.ProductId);
 return View(order);
 }

 [HttpPost]
 [ValidateAntiForgeryToken]
 public async Task<IActionResult> Edit(Order order)
 {
 if (ModelState.IsValid)
 {
 await _orderRepository.EditOrderAsync(order);
 return RedirectToAction(nameof(Index));
 }
 ViewBag.Customers = new SelectList(await _customerRepository.GetAllCustomersAsync(), "Id", "FirstName", order.CustomerId);
 ViewBag.Products = new SelectList(await _productRepository.GetAllProductsAsync(), "Id", "ProductName", order.ProductId);
 return View(order);
 }

 public async Task<IActionResult> Delete(int id)
 {
 var order = await _orderRepository.GetOrderByIdAsync(id);
 if (order == null) return NotFound();
 return View(order);
 }

 [HttpPost, ActionName("Delete")]
 [ValidateAntiForgeryToken]
 public async Task<IActionResult> DeleteConfirmed(int id)
 {
 await _orderRepository.DeleteOrderAsync(id);
 return RedirectToAction(nameof(Index));
 }
 }
}
