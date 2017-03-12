namespace FluffyCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ticketuid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "CreatedBy", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tickets", "CreatedBy");
        }
    }
}
