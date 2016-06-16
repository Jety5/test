namespace Kino.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Film : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Film", "Zdjecie", c => c.String(unicode: false));
            AddColumn("dbo.Film", "Wiek", c => c.String(unicode: false));
            AddColumn("dbo.Film", "Czas", c => c.String(unicode: false));
            AddColumn("dbo.Film", "Zdjeciedrugie", c => c.String(unicode: false));
            AddColumn("dbo.Film", "Produkcja", c => c.String(unicode: false));
            AddColumn("dbo.Film", "Obsada", c => c.String(unicode: false));
            AddColumn("dbo.Film", "Gatunek", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Film", "Gatunek");
            DropColumn("dbo.Film", "Obsada");
            DropColumn("dbo.Film", "Produkcja");
            DropColumn("dbo.Film", "Zdjeciedrugie");
            DropColumn("dbo.Film", "Czas");
            DropColumn("dbo.Film", "Wiek");
            DropColumn("dbo.Film", "Zdjecie");
        }
    }
}
