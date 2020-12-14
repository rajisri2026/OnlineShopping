using OnlineShopping.DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.ViewModel
{
    public class CartViewModel
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string UserName { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
