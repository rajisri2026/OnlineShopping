using OnlineShopping.DomainLayer;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.ViewModel
{
    public class ProductViewModel
    {
        
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
