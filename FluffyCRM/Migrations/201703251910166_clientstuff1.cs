namespace FluffyCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class clientstuff1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ClientUsers", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.ClientUsers", new[] { "UserId", "ClientId", "ContactId" }, unique: true, name: "IXUserAndClientId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ClientUsers", "IXUserAndClientId");
            AlterColumn("dbo.ClientUsers", "UserId", c => c.String(maxLength: 128));
        }
    }
}
