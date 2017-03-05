namespace FluffyCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class clientstuff3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClientUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        ClientId = c.Int(nullable: false),
                        ContactId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContactLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContactId = c.Int(nullable: false),
                        ContactLogType = c.Int(nullable: false),
                        Subject = c.String(nullable: false, maxLength: 50),
                        Comment = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        TicketId = c.Int(nullable: false),
                        ActionRequired = c.Int(nullable: false),
                        ActionComment = c.String(),
                        CallBackDue = c.DateTime(nullable: false),
                        RouteToUser = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        Address1 = c.String(maxLength: 150),
                        Address2 = c.String(maxLength: 150),
                        City = c.String(maxLength: 50),
                        State = c.String(maxLength: 50),
                        Zip = c.String(maxLength: 10),
                        Phone1 = c.String(maxLength: 50),
                        PhoneType1 = c.Int(nullable: false),
                        ClientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContactTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContactTypeName = c.String(maxLength: 50),
                        Visibility = c.Int(nullable: false),
                        delete_ind = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Clients", "FirstName");
            DropColumn("dbo.Clients", "LastName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clients", "LastName", c => c.String(maxLength: 50));
            AddColumn("dbo.Clients", "FirstName", c => c.String(maxLength: 50));
            DropTable("dbo.ContactTypes");
            DropTable("dbo.Contacts");
            DropTable("dbo.ContactLogs");
            DropTable("dbo.ClientUsers");
        }
    }
}
