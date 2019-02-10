namespace FluffyCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cards2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cards", "TaskId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cards", "TaskId");
        }
    }
}
