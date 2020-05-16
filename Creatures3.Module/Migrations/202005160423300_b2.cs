namespace Creatures3.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class b2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.cats", "TagB", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.cats", "TagB");
        }
    }
}
