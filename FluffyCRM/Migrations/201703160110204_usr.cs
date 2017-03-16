namespace FluffyCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usr : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClientUsers", "VerificationCode", c => c.String(maxLength: 16));
            AddColumn("dbo.ClientUsers", "Verified", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ClientUsers", "Verified");
            DropColumn("dbo.ClientUsers", "VerificationCode");
        }
    }
}
