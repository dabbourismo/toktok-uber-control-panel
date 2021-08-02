namespace ElCaptain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gorejgiorjeiugrheuighreujigre : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Owners",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Phone = c.String(),
                        Username = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.DeliveryMen", "OwnerId", c => c.Int(nullable: false));
            AddColumn("dbo.DeliveryMen", "VehicleNumber", c => c.String());
            AddColumn("dbo.DeliveryMen", "VehicleType", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Orders", "VehicleType", c => c.Int(nullable: false));
            CreateIndex("dbo.DeliveryMen", "OwnerId");
            AddForeignKey("dbo.DeliveryMen", "OwnerId", "dbo.Owners", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DeliveryMen", "OwnerId", "dbo.Owners");
            DropIndex("dbo.DeliveryMen", new[] { "OwnerId" });
            DropColumn("dbo.Orders", "VehicleType");
            DropColumn("dbo.Orders", "StartDate");
            DropColumn("dbo.DeliveryMen", "VehicleType");
            DropColumn("dbo.DeliveryMen", "VehicleNumber");
            DropColumn("dbo.DeliveryMen", "OwnerId");
            DropTable("dbo.Owners");
        }
    }
}
