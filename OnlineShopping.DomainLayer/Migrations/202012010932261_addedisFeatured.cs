﻿namespace OnlineShopping.DomainLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedisFeatured : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "isFeatured", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "isFeatured");
        }
    }
}
