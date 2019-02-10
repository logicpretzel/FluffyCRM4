namespace FluffyCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removecardbkg : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Cards", "BackgroundColor");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cards", "BackgroundColor", c => c.Int());
        }
    }
}
