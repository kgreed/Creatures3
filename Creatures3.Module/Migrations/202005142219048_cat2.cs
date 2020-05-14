namespace Creatures3.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cat2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.cats", "tag1", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.cats", "tag1");
        }
    }
}
