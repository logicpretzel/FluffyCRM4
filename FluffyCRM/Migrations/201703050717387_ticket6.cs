namespace FluffyCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ticket6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "LocalTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tickets", "LocalTime");
        }
    }
}
