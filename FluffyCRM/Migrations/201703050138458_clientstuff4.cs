namespace FluffyCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class clientstuff4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClientPhones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Phone = c.String(maxLength: 50),
                        PhoneType = c.Int(nullable: false),
                        ClientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            AddColumn("dbo.ContactLogs", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.ContactPhones", "ContactId");
            AddForeignKey("dbo.ContactPhones", "ContactId", "dbo.Contacts", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContactPhones", "ContactId", "dbo.Contacts");
            DropForeignKey("dbo.ClientPhones", "ClientId", "dbo.Clients");
            DropIndex("dbo.ContactPhones", new[] { "ContactId" });
            DropIndex("dbo.ClientPhones", new[] { "ClientId" });
            DropColumn("dbo.ContactLogs", "UserId");
            DropTable("dbo.ClientPhones");
        }
    }
}
