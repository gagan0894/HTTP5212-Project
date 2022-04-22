namespace HTTP5212_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createAuthor : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        AuthorId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.AuthorId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Authors");
        }
    }
}
