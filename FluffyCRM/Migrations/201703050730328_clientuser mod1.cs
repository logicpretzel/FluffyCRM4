namespace FluffyCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class clientusermod1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ClientUsers", "ClientId", c => c.Int());
            AlterColumn("dbo.ClientUsers", "ContactId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ClientUsers", "ContactId", c => c.Int(nullable: false));
            AlterColumn("dbo.ClientUsers", "ClientId", c => c.Int(nullable: false));
        }
    }
}
