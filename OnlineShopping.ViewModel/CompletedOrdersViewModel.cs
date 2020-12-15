using OnlineShopping.DomainLayer;
using System;
using System.Collections.Generic;

namespace OnlineShopping.ViewModel
{
    public class CompletedOrdersViewModel
    {
        public int CompletedOrderId { get; set; }
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderCreatedOn { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDeliveredOn { get; set; }
    }
}
