using System.ComponentModel.DataAnnotations;

namespace CWRetail.Products.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public Type Type { get; set; }
        
        public bool IsActive { get; set; }
    }
}
