namespace FluffyCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class proj : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JobTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        Description = c.String(),
                        ProdId = c.Int(),
                        ProjectId = c.Int(),
                        ParentTaskId = c.Int(),
                        Level = c.Int(),
                        ClientId = c.Int(),
                        ContactUserId = c.String(maxLength: 128),
                        CreatedBy = c.String(maxLength: 128),
                        StartDate = c.DateTime(),
                        CompletedDate = c.DateTime(),
                        DueDate = c.DateTime(),
                        LocalTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WorkProjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        ProdId = c.Int(),
                        Version = c.String(maxLength: 50),
                        ProjType = c.Int(),
                        Description = c.String(),
                        CreatedBy = c.String(maxLength: 128),
                        StartDate = c.DateTime(),
                        CompletedDate = c.DateTime(),
                        DueDate = c.DateTime(),
                        LocalTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WorkProjects");
            DropTable("dbo.JobTasks");
        }
    }
}
