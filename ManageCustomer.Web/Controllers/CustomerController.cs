using ManageCustomer.Domain.Entities;
using ManageCustomer.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ManageCustomer.Web.Controllers
{
 public class CustomerController : Controller
 {
 private readonly ICustomerRepository _customerRepository;

 public CustomerController(ICustomerRepository customerRepository)
 {
 _customerRepository = customerRepository;
 }

 public async Task<IActionResult> Index(string? searchTerm, int page =1, int pageSize =10)
 {
 IEnumerable<Customer> customers;
 if (!string.IsNullOrWhiteSpace(searchTerm))
 {
 customers = await _customerRepository.SearchCustomersAsync(searchTerm);
 }
 else
 {
 customers = await _customerRepository.GetAllCustomersAsync();
 }
 ViewBag.SearchTerm = searchTerm;
 int totalItems = customers.Count();
 var pagedCustomers = customers.Skip((page -1) * pageSize).Take(pageSize).ToList();
 ViewBag.Page = page;
 ViewBag.PageSize = pageSize;
 ViewBag.TotalItems = totalItems;
 return View(pagedCustomers);
 }

 public IActionResult Create()
 {
 return View();
 }

 [HttpPost]
 [ValidateAntiForgeryToken]
 public async Task<IActionResult> Create(Customer customer)
 {
 if (ModelState.IsValid)
 {
 await _customerRepository.AddCustomerAsync(customer);
 return RedirectToAction(nameof(Index));
 }
 return View(customer);
 }

 public async Task<IActionResult> Edit(int id)
 {
 var customer = await _customerRepository.GetCustomerByIdAsync(id);
 if (customer == null) return NotFound();
 return View(customer);
 }

 [HttpPost]
 [ValidateAntiForgeryToken]
 public async Task<IActionResult> Edit(Customer customer)
 {
 if (ModelState.IsValid)
 {
 await _customerRepository.EditCustomerAsync(customer);
 return RedirectToAction(nameof(Index));
 }
 return View(customer);
 }

 public async Task<IActionResult> Delete(int id)
 {
 var customer = await _customerRepository.GetCustomerByIdAsync(id);
 if (customer == null) return NotFound();
 return View(customer);
 }

 [HttpPost, ActionName("Delete")]
 [ValidateAntiForgeryToken]
 public async Task<IActionResult> DeleteConfirmed(int id)
 {
 var deleted = await _customerRepository.DeleteCustomerAsync(id);
 return RedirectToAction(nameof(Index));
 }
 }
}
