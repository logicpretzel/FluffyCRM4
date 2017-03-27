namespace FluffyCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tasknotes2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TaskNotes", "JobTask_Id", "dbo.JobTasks");
            DropIndex("dbo.TaskNotes", new[] { "JobTask_Id" });
            AddColumn("dbo.TaskNotes", "JobTask_Id1", c => c.Int());
            AlterColumn("dbo.TaskNotes", "JobTask_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.TaskNotes", "JobTask_Id1");
            AddForeignKey("dbo.TaskNotes", "JobTask_Id1", "dbo.JobTasks", "Id");
            DropColumn("dbo.TaskNotes", "ParentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TaskNotes", "ParentId", c => c.Int(nullable: false));
            DropForeignKey("dbo.TaskNotes", "JobTask_Id1", "dbo.JobTasks");
            DropIndex("dbo.TaskNotes", new[] { "JobTask_Id1" });
            AlterColumn("dbo.TaskNotes", "JobTask_Id", c => c.Int());
            DropColumn("dbo.TaskNotes", "JobTask_Id1");
            CreateIndex("dbo.TaskNotes", "JobTask_Id");
            AddForeignKey("dbo.TaskNotes", "JobTask_Id", "dbo.JobTasks", "Id");
        }
    }
}
