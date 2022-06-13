namespace Prose.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix_maxlength_password_unique_username : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User", "Password", c => c.String(nullable: false, maxLength: 64));
            CreateIndex("dbo.User", "Username", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.User", new[] { "Username" });
            AlterColumn("dbo.User", "Password", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
