namespace FluffyCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class q : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TaskAssignments", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TaskAssignments", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
    }
}
