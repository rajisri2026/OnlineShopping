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

        public Cart GetCart(int cartId)
        {
            return onlineShoppingDbContext.Carts.Single(model => model.CartId == cartId);
        }
        public Product GetProduct(int id)
        {
            return onlineShoppingDbContext.Products.Single(model => model.ProductId == id);
        }
        public bool AlreadyExists(int id, string username)
        {
            return onlineShoppingDbContext.Carts.Any(x => x.ProductId == id && x.Username==username);
        }
        public void StoreToCartDb(Cart cart)
        {
            onlineShoppingDbContext.Carts.Add(cart);
            onlineShoppingDbContext.SaveChanges();
        }
        public void UpdateCartDb(Cart cart)
        {
            onlineShoppingDbContext.Entry(cart).State = EntityState.Modified;
            onlineShoppingDbContext.SaveChanges();
        }
        public List<Cart> ShowCartFromDb(string userName)
        {
            List<Cart> carts = onlineShoppingDbContext.Carts.Where(x => x.Username == userName).ToList();
            return carts;
        }

        public void RemoveFromCart(int cartId)
        {
            Cart cart = onlineShoppingDbContext.Carts.Find(cartId);
            onlineShoppingDbContext.Carts.Remove(cart);
            onlineShoppingDbContext.SaveChanges();
        }
        //public void DecreaseQuantity(int cartId)
        //{
        //    Cart cart = onlineShoppingDbContext.Carts.Find(cartId);
        //    cart.Quantity -= 1;
        //    onlineShoppingDbContext.Entry(cart).State = EntityState.Modified;
        //    onlineShoppingDbContext.SaveChanges();
        //}
        //public void IncreaseQuantity(int cartId)
        //{
        //    Cart cart = onlineShoppingDbContext.Carts.Find(cartId);
        //    cart.Quantity += 1;
        //    onlineShoppingDbContext.Entry(cart).State = EntityState.Modified;
        //    onlineShoppingDbContext.SaveChanges();
        //}
    }
}
