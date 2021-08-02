namespace ElCaptain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nofuckidea : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "NotifType", c => c.Int(nullable: false));
            AddColumn("dbo.Notifications", "ExpireDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notifications", "ExpireDate");
            DropColumn("dbo.Notifications", "NotifType");
        }
    }
}
