namespace FluffyCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class products : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Products", newName: "ProductSolutions");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ProductSolutions", newName: "Products");
        }
    }
}
