namespace ElCaptain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addusernamnetoi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VehicleOwners", "Username", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.VehicleOwners", "Username");
        }
    }
}
