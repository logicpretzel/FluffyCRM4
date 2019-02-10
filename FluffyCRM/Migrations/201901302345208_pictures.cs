namespace FluffyCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pictures : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        RecipeID = c.Int(),
                        Size = c.Int(),
                        FileName = c.String(),
                        Width = c.Int(),
                        Height = c.Int(),
                        ContentType = c.String(),
                        Contents = c.String(),
                        ImageData = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Pictures");
        }
    }
}
