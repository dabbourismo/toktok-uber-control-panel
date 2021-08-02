namespace ElCaptain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class klopgrekojpegrojpgr : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderRefuses", "DeliveryManId", c => c.Int(nullable: false));
            CreateIndex("dbo.OrderRefuses", "DeliveryManId");
            AddForeignKey("dbo.OrderRefuses", "DeliveryManId", "dbo.DeliveryMen", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderRefuses", "DeliveryManId", "dbo.DeliveryMen");
            DropIndex("dbo.OrderRefuses", new[] { "DeliveryManId" });
            DropColumn("dbo.OrderRefuses", "DeliveryManId");
        }
    }
}
