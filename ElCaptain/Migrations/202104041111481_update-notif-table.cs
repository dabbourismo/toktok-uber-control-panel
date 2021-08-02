namespace ElCaptain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatenotiftable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "Name", c => c.String());
            AddColumn("dbo.Notifications", "Url", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notifications", "Url");
            DropColumn("dbo.Notifications", "Name");
        }
    }
}
