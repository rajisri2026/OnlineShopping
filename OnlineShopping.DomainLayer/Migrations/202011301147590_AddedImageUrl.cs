namespace OnlineShopping.DomainLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedImageUrl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "ImageURL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "ImageURL");
        }
    }
}
