namespace Creatures3.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class three : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.cats", "Tag", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.cats", "Tag");
        }
    }
}
