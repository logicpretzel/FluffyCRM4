namespace FluffyCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class phones3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ContactPhones", "Contact_Id", "dbo.Contacts");
            DropIndex("dbo.ContactPhones", new[] { "Contact_Id" });
            DropColumn("dbo.ContactPhones", "Contact_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ContactPhones", "Contact_Id", c => c.Int());
            CreateIndex("dbo.ContactPhones", "Contact_Id");
            AddForeignKey("dbo.ContactPhones", "Contact_Id", "dbo.Contacts", "Id");
        }
    }
}
