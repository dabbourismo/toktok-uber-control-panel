namespace ElCaptain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedeliveryman : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeliveryMen", "RegisterDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DeliveryMen", "RegisterDate");
        }
    }
}
