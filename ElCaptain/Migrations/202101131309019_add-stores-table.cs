namespace ElCaptain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addstorestable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Stores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Category = c.String(),
                        Name = c.String(),
                        Description = c.String(),
                        Address = c.String(),
                        Phone1 = c.String(),
                        Phone2 = c.String(),
                        City = c.Int(nullable: false),
                        RegisterDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Stores");
        }
    }
}
