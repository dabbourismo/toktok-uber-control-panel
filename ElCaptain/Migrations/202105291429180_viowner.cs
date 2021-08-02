namespace ElCaptain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class viowner : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DeliveryMen", "OwnerId", "dbo.Owners");
            DropIndex("dbo.DeliveryMen", new[] { "OwnerId" });
            AddColumn("dbo.DeliveryMen", "VehicleOwnerId", c => c.Int(nullable: false));
            CreateIndex("dbo.DeliveryMen", "VehicleOwnerId");
            AddForeignKey("dbo.DeliveryMen", "VehicleOwnerId", "dbo.VehicleOwners", "Id", cascadeDelete: true);
            DropColumn("dbo.DeliveryMen", "OwnerId");
            DropTable("dbo.Owners");
        }
        
        public override void Down()
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
            DropForeignKey("dbo.DeliveryMen", "VehicleOwnerId", "dbo.VehicleOwners");
            DropIndex("dbo.DeliveryMen", new[] { "VehicleOwnerId" });
            DropColumn("dbo.DeliveryMen", "VehicleOwnerId");
            CreateIndex("dbo.DeliveryMen", "OwnerId");
            AddForeignKey("dbo.DeliveryMen", "OwnerId", "dbo.Owners", "Id", cascadeDelete: true);
        }
    }
}
