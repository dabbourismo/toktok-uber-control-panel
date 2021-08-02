namespace ElCaptain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateorderstableisactive : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "isActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "isActive");
        }
    }
}
