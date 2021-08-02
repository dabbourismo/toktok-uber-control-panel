namespace ElCaptain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedevlierymanstatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeliveryMen", "Status", c => c.Int(nullable: false));
            DropColumn("dbo.DeliveryMen", "isOnline");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DeliveryMen", "isOnline", c => c.Boolean(nullable: false));
            DropColumn("dbo.DeliveryMen", "Status");
        }
    }
}
