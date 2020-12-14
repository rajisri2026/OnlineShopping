
using System.Collections.Generic;

namespace OnlineShopping.ViewModel
{
    public class CheckoutViewModel
    {
        public virtual List<CartViewModel> CartViewModel { get; set; }
        public virtual UserViewModel UserViewModel { get; set; }
    }
}
