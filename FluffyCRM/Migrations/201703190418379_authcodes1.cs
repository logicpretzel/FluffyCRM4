namespace FluffyCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class authcodes1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AuthCodes", "Valid", c => c.Boolean());
            AlterColumn("dbo.AuthCodes", "AcceptedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AuthCodes", "AcceptedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AuthCodes", "Valid", c => c.Boolean(nullable: false));
        }
    }
}
