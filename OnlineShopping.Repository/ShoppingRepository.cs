

using OnlineShopping.DomainLayer;
using OnlineShopping.ViewModel;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace OnlineShopping.Repository
{
    public class ShoppingRepository
    {
        OnlineShoppingDbContext onlineShoppingDbContext;
        public ShoppingRepository()
        {
            onlineShoppingDbContext = new OnlineShoppingDbContext();
        }
        public List<Cart> GetUserProducts(string username)
        {
            return onlineShoppingDbContext.Carts.Where(x => x.UserName == username).ToList();
        }
        public Product GetProduct(int productId)
        {
            return onlineShoppingDbContext.Products.Single(model => model.ProductId == productId);
        }
        public int GetUserId(string username)
        {
            User user = onlineShoppingDbContext.Users.Single(x => x.UserName == username);
            return user.UserId;
        }
        public void PlaceOrder(Order order)
        {
            onlineShoppingDbContext.Orders.Add(order);
            onlineShoppingDbContext.SaveChanges();
        }
        public void RemoveCartproducts(List<Cart> carts)
        {
            if (carts != null)
            {
                foreach (var item in carts)
                {
                    Cart cart = onlineShoppingDbContext.Carts.Find(item.CartId);
                    if (cart != null)
                    {
                        onlineShoppingDbContext.Carts.Remove(cart);
                        onlineShoppingDbContext.SaveChanges();
                    }

                }
            }
        }
        public Order GetOrders(int orderId)
        {
            Order order = onlineShoppingDbContext.Orders.Find(orderId);
            return order;
        }
        public List<Order> GetYourOrders(string username)
        {
            int userId = GetUserId(username);
            return onlineShoppingDbContext.Orders.Where(order => order.UserId == userId && order.Status != "Cancelled").ToList();
        }
        public List<OrderDetail> GetYourOrderDetails(int orderId)
        {
            List<OrderDetail> orderDetails = onlineShoppingDbContext.OrderDetails.Where(x => x.OrderId == orderId).ToList();
            return orderDetails;
        }
        public List<CompletedOrder> GetYourOrderHistory(int userId)
        {
            return onlineShoppingDbContext.CompletedOrders.Where(x => x.UserId == userId).ToList();
        }
        public void UpdateYouOrder(Order order)
        {
            onlineShoppingDbContext.Entry(order).State = EntityState.Modified;
            onlineShoppingDbContext.SaveChanges();
        }
    }
}
