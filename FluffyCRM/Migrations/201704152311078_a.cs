namespace FluffyCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TaskAssignments", "LastName", c => c.String(maxLength: 100));
            AlterColumn("dbo.TaskAssignments", "FirstName", c => c.String(maxLength: 100));
            AlterColumn("dbo.TaskAssignments", "Initials", c => c.String(maxLength: 3));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TaskAssignments", "Initials", c => c.String());
            AlterColumn("dbo.TaskAssignments", "FirstName", c => c.String());
            AlterColumn("dbo.TaskAssignments", "LastName", c => c.String());
        }
    }
}
