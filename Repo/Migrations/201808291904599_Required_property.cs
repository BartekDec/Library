namespace Repo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Required_property : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Book", "Isbn", c => c.String(nullable: false, maxLength: 13));
            AlterColumn("dbo.Book", "Title", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Book", "Author", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Book", "Publisher", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Book", "Available", c => c.String(nullable: false, maxLength: 3));
            AlterColumn("dbo.Category", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Category", "Name", c => c.String());
            AlterColumn("dbo.Book", "Available", c => c.String(maxLength: 3));
            AlterColumn("dbo.Book", "Publisher", c => c.String(maxLength: 50));
            AlterColumn("dbo.Book", "Author", c => c.String(maxLength: 200));
            AlterColumn("dbo.Book", "Title", c => c.String(maxLength: 200));
            AlterColumn("dbo.Book", "Isbn", c => c.String(maxLength: 13));
        }
    }
}
