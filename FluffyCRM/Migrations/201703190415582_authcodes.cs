namespace FluffyCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class authcodes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuthCodes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 128),
                        UserId = c.String(maxLength: 128),
                        Code = c.String(maxLength: 32),
                        Valid = c.Boolean(nullable: false),
                        AcceptedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AuthCodes");
        }
    }
}
