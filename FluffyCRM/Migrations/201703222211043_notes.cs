namespace FluffyCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(),
                        Subject = c.String(maxLength: 255),
                        Comment = c.String(),
                        ParentId = c.Int(nullable: false),
                        CreatedBy = c.String(maxLength: 128),
                        CreateDate = c.DateTime(nullable: false),
                        Status = c.Int(),
                        DeleteInd = c.Boolean(nullable: false),
                        ClientId = c.Int(),
                        StartDate = c.DateTime(),
                        CompletedDate = c.DateTime(),
                        DueDate = c.DateTime(),
                        LocalTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Notes");
        }
    }
}
