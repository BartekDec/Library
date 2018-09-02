namespace Repo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Availability2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Book", "Available");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Book", "Available", c => c.String(maxLength: 3));
        }
    }
}
