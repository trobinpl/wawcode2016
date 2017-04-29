namespace wawcode2016.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addnavigationproperties : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Defects",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Description = c.String(),
                        ImageUrl = c.String(),
                        FlatId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Flats", t => t.FlatId, cascadeDelete: true)
                .Index(t => t.FlatId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Defects", "FlatId", "dbo.Flats");
            DropIndex("dbo.Defects", new[] { "FlatId" });
            DropTable("dbo.Defects");
        }
    }
}
