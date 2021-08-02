namespace ElCaptain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a7aga : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Address2", c => c.String());
            AddColumn("dbo.Orders", "NearestMonument", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "NearestMonument");
            DropColumn("dbo.Orders", "Address2");
        }
    }
}
