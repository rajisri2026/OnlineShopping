using OnlineShopping.DomainLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Repository
{
    public class CategoryRepository
    {
        OnlineShoppingDbContext onlineShoppingDbContext;
        public CategoryRepository()
        {
            onlineShoppingDbContext = new OnlineShoppingDbContext();
        }

        public IEnumerable<Category> GetCategories()
        {
            return onlineShoppingDbContext.Categories.ToList();
            
        }

        public IEnumerable<Category> GetFeaturedCategories()
        {
            return onlineShoppingDbContext.Categories.Where(x => x.isFeatured && x.ImageURL != null).ToList();

        }

        public void SaveCategoryToDb(Category category)
        {
            onlineShoppingDbContext.Categories.Add(category);
            onlineShoppingDbContext.SaveChanges();
        }
        public Category FindById(int id)
        {
            return onlineShoppingDbContext.Categories.Find(id);
        }
        public void Edit(Category category)
        {
            onlineShoppingDbContext.Entry(category).State = EntityState.Modified;
            onlineShoppingDbContext.SaveChanges();
        }
        public void Delete(int id)
        {
            Category category = onlineShoppingDbContext.Categories.Find(id);
            onlineShoppingDbContext.Categories.Remove(category);
            onlineShoppingDbContext.SaveChanges();
        }
    }
}
