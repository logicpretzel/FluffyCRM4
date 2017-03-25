namespace FluffyCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fk_jobtask : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobTasks", "WorkProject_Id", c => c.Int());
            CreateIndex("dbo.JobTasks", "WorkProject_Id");
            AddForeignKey("dbo.JobTasks", "WorkProject_Id", "dbo.WorkProjects", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JobTasks", "WorkProject_Id", "dbo.WorkProjects");
            DropIndex("dbo.JobTasks", new[] { "WorkProject_Id" });
            DropColumn("dbo.JobTasks", "WorkProject_Id");
        }
    }
}
