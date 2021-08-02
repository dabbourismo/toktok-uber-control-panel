namespace ElCaptain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class toggledelivman : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeliveryMen", "isActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DeliveryMen", "isActive");
        }
    }
}
