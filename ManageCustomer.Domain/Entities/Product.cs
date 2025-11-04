using System.ComponentModel.DataAnnotations;

namespace ManageCustomer.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        
        [Required]
        public string ProductName { get; set; } = string.Empty;
        
        [Required]
        public string Category { get; set; } = string.Empty;
        
        [Required]
        public string Unit { get; set; } = string.Empty;
        
        [Required]
        public string Color { get; set; } = string.Empty;
    }
}