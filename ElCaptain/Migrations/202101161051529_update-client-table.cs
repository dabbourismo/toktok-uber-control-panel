namespace ElCaptain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateclienttable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        ClientId = c.Int(nullable: false),
                        StoreId = c.Int(nullable: false),
                        DeliveryManId = c.Int(nullable: false),
                        Details = c.String(),
                        AdditionalNotes = c.String(),
                        State = c.Int(nullable: false),
                        DeliveryDate = c.DateTime(nullable: false),
                        DeliveryManRating = c.Int(nullable: false),
                        ClientRating = c.Int(nullable: false),
                        FromLatitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FromLongitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ToLatitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ToLongitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.DeliveryMen", t => t.DeliveryManId, cascadeDelete: true)
                .ForeignKey("dbo.Stores", t => t.StoreId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.StoreId)
                .Index(t => t.DeliveryManId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.Orders", "DeliveryManId", "dbo.DeliveryMen");
            DropForeignKey("dbo.Orders", "ClientId", "dbo.Clients");
            DropIndex("dbo.Orders", new[] { "DeliveryManId" });
            DropIndex("dbo.Orders", new[] { "StoreId" });
            DropIndex("dbo.Orders", new[] { "ClientId" });
            DropTable("dbo.Orders");
        }
    }
}
