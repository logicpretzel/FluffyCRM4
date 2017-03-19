namespace FluffyCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class comments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TicketComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TicketId = c.Int(nullable: false),
                        Subject = c.String(maxLength: 255),
                        Description = c.String(),
                        CategoryId = c.Int(),
                        CreateDate = c.DateTime(),
                        Status = c.Int(nullable: false),
                        LocalTime = c.DateTime(),
                        DeleteInd = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TicketComments");
        }
    }
}
