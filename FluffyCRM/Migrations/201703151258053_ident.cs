namespace FluffyCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ident : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ClientID", c => c.Int());
            AddColumn("dbo.AspNetUsers", "NewClient", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "RequestInfo", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "RequestInfo");
            DropColumn("dbo.AspNetUsers", "NewClient");
            DropColumn("dbo.AspNetUsers", "ClientID");
        }
    }
}
