namespace Repo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Availability : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Book", "Available", c => c.String(maxLength: 3));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Book", "Available");
        }
    }
}
