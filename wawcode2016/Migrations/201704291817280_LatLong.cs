namespace wawcode2016.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LatLong : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Flats", "Latitude", c => c.String());
            AddColumn("dbo.Flats", "Longitude", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Flats", "Longitude");
            DropColumn("dbo.Flats", "Latitude");
        }
    }
}
