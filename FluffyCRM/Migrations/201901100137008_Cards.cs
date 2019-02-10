namespace FluffyCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cards : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreateDt = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                        ProjectId = c.Int(defaultValueSql: "0"),
                        EntityId = c.Int(defaultValueSql: "0"),
                        Title = c.String(maxLength: 255),
                        Detail = c.String(unicode: false),
                        SprintId = c.Int(defaultValueSql: "0"),
                        HasChildren = c.Boolean(defaultValueSql: "0"),
                        HasPrereqs = c.Boolean(defaultValueSql: "0"),
                        ExpStartDt = c.DateTime(),
                        ExpStopDt = c.DateTime(),
                        StartDt = c.DateTime(),
                        CompletedDt = c.DateTime(),
                        BudgetHours = c.Decimal(defaultValueSql: "0",precision: 18, scale: 2),
                        ActualHours = c.Decimal(defaultValueSql: "0",precision: 18, scale: 2),
                        ModifyDt = c.DateTime(),
                        ModifyBy = c.Int(defaultValueSql: "0"),
                        ParentId = c.Int(defaultValueSql: "0"),
                        CatId = c.Int(defaultValueSql: "0"),
                        BackgroundColor = c.Int(defaultValueSql: "0"),
                        StatusId = c.Int(defaultValueSql: "0"),
                        DeleteId = c.Int(defaultValueSql: "0"),
                        DeletedDt = c.DateTime(),
                        ArchivedInd = c.Boolean(defaultValueSql: "0"),
                        ArchivedDt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Cards");
        }
    }
}
