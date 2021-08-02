namespace ElCaptain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateorderstoreid : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "StoreId", "dbo.Stores");
            DropIndex("dbo.Orders", new[] { "StoreId" });
            AlterColumn("dbo.Orders", "StoreId", c => c.Int());
            CreateIndex("dbo.Orders", "StoreId");
            AddForeignKey("dbo.Orders", "StoreId", "dbo.Stores", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "StoreId", "dbo.Stores");
            DropIndex("dbo.Orders", new[] { "StoreId" });
            AlterColumn("dbo.Orders", "StoreId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "StoreId");
            AddForeignKey("dbo.Orders", "StoreId", "dbo.Stores", "Id", cascadeDelete: true);
        }
    }
}
