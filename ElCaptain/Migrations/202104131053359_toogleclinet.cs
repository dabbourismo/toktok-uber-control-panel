namespace ElCaptain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class toogleclinet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "isActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "isActive");
        }
    }
}
