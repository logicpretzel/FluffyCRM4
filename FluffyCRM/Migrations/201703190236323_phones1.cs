namespace FluffyCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class phones1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContactPhones", "Comment", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContactPhones", "Comment");
        }
    }
}
