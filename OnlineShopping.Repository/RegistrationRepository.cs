using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShopping.DomainLayer;

namespace OnlineShopping.Repository
{
    public class RegistrationRepository
    {
        OnlineShoppingDbContext onlineShoppingDbContext;
        public RegistrationRepository()
        {
            onlineShoppingDbContext = new OnlineShoppingDbContext();
        }
        public void AnyUser(User user, out bool email, out bool userName)
        {
            email = onlineShoppingDbContext.Users.Any(x => x.UserEmail == user.UserEmail);
            userName = onlineShoppingDbContext.Users.Any(x => x.UserName == user.UserName);
        }
        public void Create(User user)
        {
            onlineShoppingDbContext.Users.Add(user);
            onlineShoppingDbContext.SaveChanges();
        }
        public bool IsRegisteredUser(User user)
        {
            return onlineShoppingDbContext.Users.Any(x => x.Password == user.Password && x.UserName == user.UserName);
        }
    }
}
