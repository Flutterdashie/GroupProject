namespace GroupProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nowblogstoo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlogPosts",
                c => new
                    {
                        BlogPostId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Message = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                        LastEdited = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BlogPostId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BlogPosts");
        }
    }
}
