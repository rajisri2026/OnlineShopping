

using OnlineShopping.DomainLayer;
using System.Collections.Generic;
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
    }
}
