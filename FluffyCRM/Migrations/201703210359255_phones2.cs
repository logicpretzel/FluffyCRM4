namespace FluffyCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class phones2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contacts", "Email", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contacts", "Email");
        }
    }
}
