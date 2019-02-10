namespace FluffyCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class note3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TaskNotes", "LocalTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TaskNotes", "LocalTime", c => c.DateTime());
        }
    }
}
