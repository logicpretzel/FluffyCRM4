namespace FluffyCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tasknotes3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TaskNotes", "CreateDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TaskNotes", "CreateDate", c => c.DateTime(nullable: false));
        }
    }
}
