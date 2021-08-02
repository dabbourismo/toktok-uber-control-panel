namespace ElCaptain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jgiaonjgiokewanmgikoewm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeliveryMen", "LastUpdate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DeliveryMen", "LastUpdate");
        }
    }
}
