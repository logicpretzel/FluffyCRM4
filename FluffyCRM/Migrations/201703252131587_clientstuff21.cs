namespace FluffyCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class clientstuff21 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TaskAssignments", "Initials", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TaskAssignments", "Initials");
        }
    }
}
