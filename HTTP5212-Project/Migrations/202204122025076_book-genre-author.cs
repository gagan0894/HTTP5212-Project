namespace HTTP5212_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bookgenreauthor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "GenreId", c => c.Int(nullable: false));
            AddColumn("dbo.Books", "AuthorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Books", "GenreId");
            CreateIndex("dbo.Books", "AuthorId");
            AddForeignKey("dbo.Books", "AuthorId", "dbo.Authors", "AuthorId", cascadeDelete: true);
            AddForeignKey("dbo.Books", "GenreId", "dbo.Genres", "GenreId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.Books", "AuthorId", "dbo.Authors");
            DropIndex("dbo.Books", new[] { "AuthorId" });
            DropIndex("dbo.Books", new[] { "GenreId" });
            DropColumn("dbo.Books", "AuthorId");
            DropColumn("dbo.Books", "GenreId");
        }
    }
}
