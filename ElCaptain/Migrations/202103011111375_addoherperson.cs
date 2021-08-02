namespace ElCaptain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addoherperson : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "OtherPerson", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "OtherPerson");
        }
    }
}
