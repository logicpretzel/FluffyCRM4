namespace FluffyCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ticketdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tickets", "CreateDate", c => c.DateTime(defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.Tickets", "StartDate", c => c.DateTime());
            AlterColumn("dbo.Tickets", "CompletedDate", c => c.DateTime());
            AlterColumn("dbo.Tickets", "DueDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tickets", "DueDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Tickets", "CompletedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Tickets", "StartDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Tickets", "CreateDate", c => c.DateTime(nullable: false));
        }
    }
}
