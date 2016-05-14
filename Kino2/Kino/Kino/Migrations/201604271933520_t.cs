namespace Kino.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class t : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Film", "GodzinaZakonczenia");
        }
        
        public override void Down()
        {
        }
    }
}
