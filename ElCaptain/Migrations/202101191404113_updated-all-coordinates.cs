namespace ElCaptain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedallcoordinates : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.Clients", "Latitude", c => c.Double(nullable: false));
            AlterColumn("dbo.Clients", "Longitude", c => c.Double(nullable: false));
            AlterColumn("dbo.DeliveryMen", "Latitude", c => c.Double(nullable: false));
            AlterColumn("dbo.DeliveryMen", "Longitude", c => c.Double(nullable: false));
            AlterColumn("dbo.Orders", "FromLatitude", c => c.Double(nullable: false));
            AlterColumn("dbo.Orders", "FromLongitude", c => c.Double(nullable: false));
            AlterColumn("dbo.Orders", "ToLatitude", c => c.Double(nullable: false));
            AlterColumn("dbo.Orders", "ToLongitude", c => c.Double(nullable: false));
            DropColumn("dbo.Stores", "Latitude");
            DropColumn("dbo.Stores", "Longitude");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Stores", "Longitude", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Stores", "Latitude", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Orders", "ToLongitude", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Orders", "ToLatitude", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Orders", "FromLongitude", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Orders", "FromLatitude", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.DeliveryMen", "Longitude", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.DeliveryMen", "Latitude", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Clients", "Longitude", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Clients", "Latitude", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropTable("dbo.Notifications");
        }
    }
}
