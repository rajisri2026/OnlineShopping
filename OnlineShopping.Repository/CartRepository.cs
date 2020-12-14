using OnlineShopping.DomainLayer;
using OnlineShopping.ViewModel;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace OnlineShopping.Repository
{
    public class CartRepository
    {
        OnlineShoppingDbContext onlineShoppingDbContext;
        
        public CartRepository()
        {
            onlineShoppingDbContext = new OnlineShoppingDbContext();
        }

        public Cart GetCart(int CartId)
        {
            return onlineShoppingDbContext.Carts.Single(model => model.CartId == CartId);
        }
        public Cart GetCart(int productId, string username)
        {
            return onlineShoppingDbContext.Carts.Single(model => model.ProductId == productId && model.UserName == username);
        }
        public int GetUserId(string username)
        {
            User user = onlineShoppingDbContext.Users.Single(x => x.UserName == username);
            return user.UserId;
        }
        public Product GetProduct(int id)
        {
            return onlineShoppingDbContext.Products.Single(model => model.ProductId == id);
        }
        public bool AlreadyExists(int id, string username)
        {
            return onlineShoppingDbContext.Carts.Any(x => x.ProductId == id && x.UserName==username);

        }
        public void StoreToCartDb(Cart Cart)
        {
            onlineShoppingDbContext.Carts.Add(Cart);
            onlineShoppingDbContext.SaveChanges();
        }
        public void UpdateCartDb(Cart Cart)
        {
            onlineShoppingDbContext.Entry(Cart).State = EntityState.Modified;
            onlineShoppingDbContext.SaveChanges();
        }
        public List<Cart> ShowCartFromDb(string userName)
        {
            List<Cart> Carts = onlineShoppingDbContext.Carts.Where(x => x.UserName == userName).ToList();
            return Carts;
        }

        public void RemoveFromCart(int CartId)
        {
            Cart Cart = onlineShoppingDbContext.Carts.Find(CartId);
            onlineShoppingDbContext.Carts.Remove(Cart);
            onlineShoppingDbContext.SaveChanges();
        }
        
    }
}
