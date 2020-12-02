using OnlineShopping.DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.ViewModel
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ImageURL { get; set; }
        public List<Product> Products { get; set; }
        public bool isFeatured { get; set; }
    }
}
