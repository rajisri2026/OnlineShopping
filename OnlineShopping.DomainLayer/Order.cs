using System;
using System.Collections.Generic;

namespace OnlineShopping.DomainLayer
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderCreatedOn { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public virtual  List<OrderDetail> OrderDetail { get; set; }
    }
}
