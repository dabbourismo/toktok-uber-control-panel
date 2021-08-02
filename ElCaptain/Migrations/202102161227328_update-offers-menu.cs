namespace ElCaptain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateoffersmenu : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Offers", "StoreId", "dbo.Stores");
            DropIndex("dbo.Offers", new[] { "StoreId" });
            CreateTable(
                "dbo.Menus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        Title = c.String(),
                        isOffered = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Stores", t => t.StoreId, cascadeDelete: true)
                .Index(t => t.StoreId);
            
            DropTable("dbo.Offers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Offers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        isDone = c.Boolean(nullable: false),
                        StoreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Menus", "StoreId", "dbo.Stores");
            DropIndex("dbo.Menus", new[] { "StoreId" });
            DropTable("dbo.Menus");
            CreateIndex("dbo.Offers", "StoreId");
            AddForeignKey("dbo.Offers", "StoreId", "dbo.Stores", "Id", cascadeDelete: true);
        }
    }
}
