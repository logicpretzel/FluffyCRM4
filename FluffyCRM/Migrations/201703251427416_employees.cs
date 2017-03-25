namespace FluffyCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class employees : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 100),
                        LastName = c.String(maxLength: 100),
                        Initials = c.String(maxLength: 3),
                        JobTitle = c.String(maxLength: 100),
                        UserId = c.String(maxLength: 128),
                        Address = c.String(maxLength: 150),
                        City = c.String(maxLength: 50),
                        State = c.String(maxLength: 50),
                        Zip = c.String(maxLength: 10),
                        Phone1 = c.String(maxLength: 50),
                        PhoneType1 = c.Int(),
                        Phone2 = c.String(maxLength: 50),
                        PhoneType2 = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.TaskAssignments", "LastName", c => c.String());
            AddColumn("dbo.TaskAssignments", "FirstName", c => c.String());
            AddColumn("dbo.TaskAssignments", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Clients", "PhoneType1", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clients", "PhoneType1", c => c.Int(nullable: false));
            DropColumn("dbo.TaskAssignments", "Discriminator");
            DropColumn("dbo.TaskAssignments", "FirstName");
            DropColumn("dbo.TaskAssignments", "LastName");
            DropTable("dbo.Employees");
        }
    }
}
