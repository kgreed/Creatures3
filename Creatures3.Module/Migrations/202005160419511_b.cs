namespace Creatures3.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class b : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.cats", "TagA", c => c.String());
            DropColumn("dbo.cats", "Tag");
        }
        
        public override void Down()
        {
            AddColumn("dbo.cats", "Tag", c => c.String());
            DropColumn("dbo.cats", "TagA");
        }
    }
}
