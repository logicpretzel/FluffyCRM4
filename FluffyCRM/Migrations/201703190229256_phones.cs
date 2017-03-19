namespace FluffyCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class phones : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ClientPhones", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.ContactPhones", "ContactId", "dbo.Contacts");
            DropIndex("dbo.ClientPhones", new[] { "ClientId" });
            DropIndex("dbo.ContactPhones", new[] { "ContactId" });
            RenameColumn(table: "dbo.ContactPhones", name: "ContactId", newName: "Contact_Id");
            AddColumn("dbo.ContactPhones", "ParentId", c => c.Int(nullable: false));
            AddColumn("dbo.ContactPhones", "ParentRecordType", c => c.Int(nullable: false));
            AlterColumn("dbo.ContactPhones", "Contact_Id", c => c.Int());
            CreateIndex("dbo.ContactPhones", "Contact_Id");
            AddForeignKey("dbo.ContactPhones", "Contact_Id", "dbo.Contacts", "Id");
            DropTable("dbo.ClientPhones");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.ContactPhones", "Contact_Id", "dbo.Contacts");
            DropIndex("dbo.ContactPhones", new[] { "Contact_Id" });
            AlterColumn("dbo.ContactPhones", "Contact_Id", c => c.Int(nullable: false));
            DropColumn("dbo.ContactPhones", "ParentRecordType");
            DropColumn("dbo.ContactPhones", "ParentId");
            RenameColumn(table: "dbo.ContactPhones", name: "Contact_Id", newName: "ContactId");
            CreateIndex("dbo.ContactPhones", "ContactId");
            CreateIndex("dbo.ClientPhones", "ClientId");
            AddForeignKey("dbo.ContactPhones", "ContactId", "dbo.Contacts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ClientPhones", "ClientId", "dbo.Clients", "ClientId", cascadeDelete: true);
        }
    }
}
