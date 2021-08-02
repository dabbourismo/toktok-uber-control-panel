namespace ElCaptain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pricingtable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pricing",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Pricing1 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Pricing2 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Pricing3 = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Pricing");
        }
    }
}
