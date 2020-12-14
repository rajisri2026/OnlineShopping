namespace OnlineShopping.DomainLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cartdeleted : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Carts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Carts", "UserId", "dbo.Users");
            DropIndex("dbo.Carts", new[] { "ProductId" });
            DropIndex("dbo.Carts", new[] { "UserId" });
            DropTable("dbo.Carts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        CartId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Username = c.String(),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CartId);
            
            CreateIndex("dbo.Carts", "UserId");
            CreateIndex("dbo.Carts", "ProductId");
            AddForeignKey("dbo.Carts", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.Carts", "ProductId", "dbo.Products", "ProductId", cascadeDelete: true);
        }
    }
}
