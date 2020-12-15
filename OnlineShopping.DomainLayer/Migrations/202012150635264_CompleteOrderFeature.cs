namespace OnlineShopping.DomainLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompleteOrderFeature : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CompletedOrders",
                c => new
                    {
                        CompletedOrderId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        OrderCreatedOn = c.DateTime(nullable: false),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderDeliveredOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CompletedOrderId);
            
            AddColumn("dbo.OrderDetails", "CompletedOrder_CompletedOrderId", c => c.Int());
            CreateIndex("dbo.OrderDetails", "CompletedOrder_CompletedOrderId");
            AddForeignKey("dbo.OrderDetails", "CompletedOrder_CompletedOrderId", "dbo.CompletedOrders", "CompletedOrderId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "CompletedOrder_CompletedOrderId", "dbo.CompletedOrders");
            DropIndex("dbo.OrderDetails", new[] { "CompletedOrder_CompletedOrderId" });
            DropColumn("dbo.OrderDetails", "CompletedOrder_CompletedOrderId");
            DropTable("dbo.CompletedOrders");
        }
    }
}
