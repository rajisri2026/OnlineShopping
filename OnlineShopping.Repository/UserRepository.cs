
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using OnlineShopping.DomainLayer;

namespace OnlineShopping.Repository
{
    public class UserRepository
    {
        OnlineShoppingDbContext onlineShoppingDbContext;
        public UserRepository()
        {
            onlineShoppingDbContext = new OnlineShoppingDbContext();
        }
        public IEnumerable<User> DisplayAll()
        {
             
            IEnumerable<User> user = onlineShoppingDbContext.Users.ToList();
               return user;
            
        }

        public IEnumerable<Role> GetRoles()
        {
            IEnumerable<Role> role = onlineShoppingDbContext.Roles.ToList();
            return role;
        }
        public void Create(User user)
        { 
            onlineShoppingDbContext.Users.Add(user);
            Save();
        }
        public void Edit(User user)
        {
            onlineShoppingDbContext.Entry(user).State = EntityState.Modified;
            Save();
        }
        public void Delete(int id)
        {
            User user = onlineShoppingDbContext.Users.Find(id);
            onlineShoppingDbContext.Users.Remove(user);
            Save();
        }

        public User Detail(int id)
        {
            return onlineShoppingDbContext.Users.Find(id);
        }
        public void Save()
        {
            onlineShoppingDbContext.SaveChanges();
        }
    }
}
