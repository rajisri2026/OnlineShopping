﻿
using System.Data.Entity;
namespace OnlineShopping.DomainLayer
{
    public class OnlineShoppingDbContext : DbContext
    {
        public OnlineShoppingDbContext() : base("OnlineShoppingDbContext") { }
        
      
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

    }
}