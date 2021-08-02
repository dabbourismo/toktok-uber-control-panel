namespace ElCaptain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addorderrefuses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderRefuses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderRefuses", "OrderId", "dbo.Orders");
            DropIndex("dbo.OrderRefuses", new[] { "OrderId" });
            DropTable("dbo.OrderRefuses");
        }
    }
}
