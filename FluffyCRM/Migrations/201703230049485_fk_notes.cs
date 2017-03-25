namespace FluffyCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fk_notes : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Notes", newName: "TaskNotes");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.TaskNotes", newName: "Notes");
        }
    }
}
