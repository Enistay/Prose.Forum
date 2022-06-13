namespace Prose.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProseV11_rename_topic_update_to_uptadeDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Topic", "UpdateDate", c => c.DateTime());
            DropColumn("dbo.Topic", "Update");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Topic", "Update", c => c.DateTime());
            DropColumn("dbo.Topic", "UpdateDate");
        }
    }
}
