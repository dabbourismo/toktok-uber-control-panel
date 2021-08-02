namespace ElCaptain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatevowner : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VehicleOwners", "Category", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.VehicleOwners", "Category");
        }
    }
}
