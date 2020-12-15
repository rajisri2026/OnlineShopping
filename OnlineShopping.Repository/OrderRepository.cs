using OnlineShopping.DomainLayer;
using System.Collections.Generic;//To use Lists
using System.Data.Entity;//to use entitystate for update operation
using System.Linq;//To use ToList()

namespace OnlineShopping.Repository
{
    public class OrderRepository
    {
        OnlineShoppingDbContext onlineShoppingDbContext;
        public OrderRepository()
        {
            onlineShoppingDbContext = new OnlineShoppingDbContext();
        }
        public List<Order> ShowPendingOrders()
        {
            return onlineShoppingDbContext.Orders.Where(x=>x.Status=="Pending" || x.Status=="Approved" || x.Status=="Dispatched").ToList();
        }
      
        public List<OrderDetail > OrderDetails(int orderId)
        {
            return onlineShoppingDbContext.OrderDetails.Where(order => order.OrderId == orderId).ToList();
        }
        public Order GetOrderDetails(int orderId)
        {
            Order order = onlineShoppingDbContext.Orders.Find(orderId);
            return order;
        }
        public void ApproveOrder(Order order)
        {
            onlineShoppingDbContext.Entry(order).State = EntityState.Modified;
            onlineShoppingDbContext.SaveChanges();
        }
        public void CompletedOrders(Order oldOrder, CompletedOrder completedOrder)
        {
            onlineShoppingDbContext.CompletedOrders.Add(completedOrder);
            onlineShoppingDbContext.Orders.Remove(oldOrder);
            onlineShoppingDbContext.SaveChanges();
        }
        public List<CompletedOrder> ShowCompletedOrders()
        {
            return onlineShoppingDbContext.CompletedOrders.ToList();
        }
        public List<Order> CancelledOrders()
        {
            return onlineShoppingDbContext.Orders.Where(x => x.Status == "Cancelled").ToList();
        }
    }
}
