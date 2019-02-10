namespace FluffyCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pictures2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Pictures", "RecipeID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pictures", "RecipeID", c => c.Int());
        }
    }
}
