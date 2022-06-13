namespace Prose.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProseV1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Topic",
                c => new
                    {
                        TopicId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 150),
                        Text = c.String(nullable: false),
                        Creation = c.DateTime(nullable: false),
                        Update = c.DateTime(),
                        Enable = c.Boolean(nullable: false),
                        Keyword = c.String(maxLength: 150),
                        UserId = c.Int(),
                    })
                .PrimaryKey(t => t.TopicId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 150),
                        Password = c.String(nullable: false, maxLength: 50),
                        Enable = c.Boolean(nullable: false),
                        Registration = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Topic", "UserId", "dbo.User");
            DropIndex("dbo.Topic", new[] { "UserId" });
            DropTable("dbo.User");
            DropTable("dbo.Topic");
        }
    }
}
