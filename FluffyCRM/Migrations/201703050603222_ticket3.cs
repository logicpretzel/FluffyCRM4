namespace FluffyCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ticket3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tickets", "ClientId", c => c.Int());
            AlterColumn("dbo.Tickets", "CreatedBy", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tickets", "CreatedBy", c => c.String(maxLength: 128));
            AlterColumn("dbo.Tickets", "ClientId", c => c.Int(nullable: false));
        }
    }
}
