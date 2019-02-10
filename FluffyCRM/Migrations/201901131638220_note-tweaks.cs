namespace FluffyCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notetweaks : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TaskNotes", "JobTask_Id1", "dbo.JobTasks");
            DropIndex("dbo.TaskNotes", new[] { "JobTask_Id1" });
            AlterColumn("dbo.TaskNotes", "Comment", c => c.String(unicode: false));
            AlterColumn("dbo.TaskNotes", "DeleteInd", c => c.Boolean());
            DropColumn("dbo.JobTasks", "LocalTime");
          // -- DropColumn("dbo.TaskNotes", "JobTask_Id1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TaskNotes", "JobTask_Id1", c => c.Int());
            AddColumn("dbo.JobTasks", "LocalTime", c => c.DateTime());
            AlterColumn("dbo.TaskNotes", "DeleteInd", c => c.Boolean(nullable: false));
            AlterColumn("dbo.TaskNotes", "Comment", c => c.String());
            CreateIndex("dbo.TaskNotes", "JobTask_Id1");
         // --  AddForeignKey("dbo.TaskNotes", "JobTask_Id1", "dbo.JobTasks", "Id");
        }
    }
}
