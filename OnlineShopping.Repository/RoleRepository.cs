using OnlineShopping.DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Repository
{
    public class RoleRepository
    {
        OnlineShoppingDbContext onlineShoppingDbContext = new OnlineShoppingDbContext();
        public IEnumerable<Role> GetRoles()
        {
            IEnumerable<Role> role = onlineShoppingDbContext.Roles.ToList();
            return role;
        }
    }
}
