namespace ElCaptain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddeliverymantable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeliveryMen",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NationalId = c.String(),
                        City = c.Int(nullable: false),
                        Address = c.String(),
                        Phone1 = c.String(),
                        Phone2 = c.String(),
                        LandPhone = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        Rating = c.Int(nullable: false),
                        isOnline = c.Boolean(nullable: false),
                        Latitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Longitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DeliveryMen");
        }
    }
}
