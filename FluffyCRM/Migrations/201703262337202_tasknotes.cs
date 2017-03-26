namespace FluffyCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tasknotes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TaskNotes", "CategoryName", c => c.String(maxLength: 100));
            AddColumn("dbo.TaskNotes", "ParentTaskName", c => c.String(maxLength: 255));
            AddColumn("dbo.TaskNotes", "CreatedByName", c => c.String(maxLength: 255));
            AddColumn("dbo.TaskNotes", "ClientName", c => c.String(maxLength: 255));
            AddColumn("dbo.TaskNotes", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TaskNotes", "Discriminator");
            DropColumn("dbo.TaskNotes", "ClientName");
            DropColumn("dbo.TaskNotes", "CreatedByName");
            DropColumn("dbo.TaskNotes", "ParentTaskName");
            DropColumn("dbo.TaskNotes", "CategoryName");
        }
    }
}
