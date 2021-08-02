namespace ElCaptain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateordernull : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "DeliveryManId", "dbo.DeliveryMen");
            DropIndex("dbo.Orders", new[] { "DeliveryManId" });
            AlterColumn("dbo.Orders", "DeliveryManId", c => c.Int());
            CreateIndex("dbo.Orders", "DeliveryManId");
            AddForeignKey("dbo.Orders", "DeliveryManId", "dbo.DeliveryMen", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "DeliveryManId", "dbo.DeliveryMen");
            DropIndex("dbo.Orders", new[] { "DeliveryManId" });
            AlterColumn("dbo.Orders", "DeliveryManId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "DeliveryManId");
            AddForeignKey("dbo.Orders", "DeliveryManId", "dbo.DeliveryMen", "Id", cascadeDelete: true);
        }
    }
}
