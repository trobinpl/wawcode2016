namespace wawcode2016.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Flats", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Flats", "ImageUrl");
        }
    }
}
