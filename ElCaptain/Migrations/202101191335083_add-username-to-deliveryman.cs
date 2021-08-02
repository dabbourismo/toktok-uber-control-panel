namespace ElCaptain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addusernametodeliveryman : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeliveryMen", "Username", c => c.String());
            AddColumn("dbo.DeliveryMen", "Password", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DeliveryMen", "Password");
            DropColumn("dbo.DeliveryMen", "Username");
        }
    }
}
