namespace FluffyCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jobs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobTasks", "TaskType", c => c.Int());
            AddColumn("dbo.JobTasks", "TicketId", c => c.Int());
            DropColumn("dbo.JobTasks", "ClientId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.JobTasks", "ClientId", c => c.Int());
            DropColumn("dbo.JobTasks", "TicketId");
            DropColumn("dbo.JobTasks", "TaskType");
        }
    }
}
