using OnlineShopping.DomainLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Repository
{
    public class ProductRepository
    {
        OnlineShoppingDbContext onlineShoppingDbContext;

        public ProductRepository()
        {
            onlineShoppingDbContext = new OnlineShoppingDbContext();
        }
        public IEnumerable<Product> DisplayAll()
        {
            IEnumerable<Product> product = onlineShoppingDbContext.Products.ToList();
            return product;
        }
        public void Create(Product product)
        {
           // onlineShoppingDbContext.Entry(product.Category).State = EntityState.Unchanged;
            onlineShoppingDbContext.Products.Add(product);
            Save();
        }
        public void Edit(Product product)
        {
            onlineShoppingDbContext.Entry(product).State = EntityState.Modified;
            Save();
        }
        public void Delete(int id)
        {
            Product product = onlineShoppingDbContext.Products.Find(id);
            onlineShoppingDbContext.Products.Remove(product);
            Save();
        }

        public Product Detail(int id)
        {
            return onlineShoppingDbContext.Products.Find(id);
        }
        public void Save()
        {
            onlineShoppingDbContext.SaveChanges();
        }
    }
}
