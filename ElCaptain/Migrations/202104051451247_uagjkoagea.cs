namespace ElCaptain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uagjkoagea : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stores", "isFree", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "ClientNotes", c => c.String());
            AddColumn("dbo.Orders", "DeliveryNotes", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "DeliveryNotes");
            DropColumn("dbo.Orders", "ClientNotes");
            DropColumn("dbo.Stores", "isFree");
        }
    }
}
