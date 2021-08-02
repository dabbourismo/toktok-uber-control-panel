namespace ElCaptain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatestorestable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stores", "Latitude", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Stores", "Longitude", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stores", "Longitude");
            DropColumn("dbo.Stores", "Latitude");
        }
    }
}
