namespace OnlineShopping.DomainLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shoppingcart : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Carts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Carts", "User_UserId", "dbo.Users");
            DropIndex("dbo.Carts", new[] { "ProductId" });
            DropIndex("dbo.Carts", new[] { "User_UserId" });
            DropTable("dbo.Carts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        CartId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.CartId);
            
            CreateIndex("dbo.Carts", "User_UserId");
            CreateIndex("dbo.Carts", "ProductId");
            AddForeignKey("dbo.Carts", "User_UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Carts", "ProductId", "dbo.Products", "ProductId", cascadeDelete: true);
        }
    }
}
