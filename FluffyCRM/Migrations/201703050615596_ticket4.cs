namespace FluffyCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ticket4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tickets", "CategoryId", c => c.Int());
            AlterColumn("dbo.Tickets", "Status", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tickets", "Status", c => c.Int(nullable: false));
            AlterColumn("dbo.Tickets", "CategoryId", c => c.Int(nullable: false));
        }
    }
}
