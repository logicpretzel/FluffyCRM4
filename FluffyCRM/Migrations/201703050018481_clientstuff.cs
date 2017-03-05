namespace FluffyCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class clientstuff : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactPhones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Phone = c.String(maxLength: 50),
                        PhoneType = c.Int(nullable: false),
                        ContactId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Clients", "CompanyName", c => c.String(maxLength: 150));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "CompanyName");
            DropTable("dbo.ContactPhones");
        }
    }
}
