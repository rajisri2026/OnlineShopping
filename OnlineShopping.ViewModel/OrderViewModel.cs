using OnlineShopping.DomainLayer;
using System;
using System.Collections.Generic;

namespace OnlineShopping.ViewModel
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderCreatedOn { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public virtual List<OrderDetail> OrderDetail { get; set; }
    }
}
