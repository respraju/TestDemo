using System;
using System.ComponentModel.DataAnnotations;

namespace ManageCustomer.Domain.Entities
{
 public class Order
 {
 public int Id { get; set; }
 [Required]
 public int CustomerId { get; set; }
 public Customer? Customer { get; set; }
 [Required]
 public int ProductId { get; set; }
 public Product? Product { get; set; }
 [Required]
 public DateTime OrderDate { get; set; }
 [Required]
 public decimal TotalAmount { get; set; }
 public string? Notes { get; set; }
 }
}
