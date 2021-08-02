namespace ElCaptain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deepweb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeliveryMen", "Category", c => c.Int(nullable: false));
            AddColumn("dbo.DeliveryMen", "DealLimit", c => c.Int(nullable: false));
            AddColumn("dbo.DeliveryMen", "NumberOfTrips", c => c.Int(nullable: false));
            AddColumn("dbo.DeliveryMen", "ChassieNumber", c => c.Int(nullable: false));
            AddColumn("dbo.DeliveryMen", "MotorNumber", c => c.Int(nullable: false));
            AddColumn("dbo.DeliveryMen", "Color", c => c.String());
            AddColumn("dbo.DeliveryMen", "TrafficDepartment", c => c.String());
            AddColumn("dbo.DeliveryMen", "InspectionDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.DeliveryMen", "LicenseEndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.DeliveryMen", "TaxEndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.DeliveryMen", "DrivingLine", c => c.String());
            AddColumn("dbo.DeliveryMen", "WorkTimes", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DeliveryMen", "WorkTimes");
            DropColumn("dbo.DeliveryMen", "DrivingLine");
            DropColumn("dbo.DeliveryMen", "TaxEndDate");
            DropColumn("dbo.DeliveryMen", "LicenseEndDate");
            DropColumn("dbo.DeliveryMen", "InspectionDate");
            DropColumn("dbo.DeliveryMen", "TrafficDepartment");
            DropColumn("dbo.DeliveryMen", "Color");
            DropColumn("dbo.DeliveryMen", "MotorNumber");
            DropColumn("dbo.DeliveryMen", "ChassieNumber");
            DropColumn("dbo.DeliveryMen", "NumberOfTrips");
            DropColumn("dbo.DeliveryMen", "DealLimit");
            DropColumn("dbo.DeliveryMen", "Category");
        }
    }
}
