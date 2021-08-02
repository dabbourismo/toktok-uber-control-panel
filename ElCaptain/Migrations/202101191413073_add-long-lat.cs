namespace ElCaptain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addlonglat : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stores", "Latitude", c => c.Double(nullable: false));
            AddColumn("dbo.Stores", "Longitude", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stores", "Longitude");
            DropColumn("dbo.Stores", "Latitude");
        }
    }
}
