
using System.Collections.Generic;

namespace OnlineShopping.ViewModel
{
    public class HomeViewModel
    {
        public IEnumerable<CategoryViewModel> FeaturedCategories { get; set; }
        public IEnumerable<ProductViewModel> FeaturedProducts { get; set; }
    }
}
