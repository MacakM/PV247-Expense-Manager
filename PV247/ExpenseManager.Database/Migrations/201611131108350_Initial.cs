namespace ExpenseManager.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountBadgeModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountId = c.Int(nullable: false),
                        BadgeId = c.Int(nullable: false),
                        Achieved = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountModels", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("dbo.BadgeModels", t => t.BadgeId, cascadeDelete: true)
                .Index(t => t.AccountId)
                .Index(t => t.BadgeId);
            
            CreateTable(
                "dbo.AccountModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CostInfoModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsIncome = c.Boolean(nullable: false),
                        Money = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                        AccountId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        TypeId = c.Int(nullable: false),
                        Periodicity = c.Int(),
                        PeriodicMultiplicity = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountModels", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("dbo.CostTypeModels", t => t.TypeId, cascadeDelete: true)
                .Index(t => t.AccountId)
                .Index(t => t.TypeId);
            
            CreateTable(
                "dbo.CostTypeModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PlanModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountId = c.Int(nullable: false),
                        Description = c.String(maxLength: 256),
                        PlanType = c.Int(nullable: false),
                        PlannedMoney = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Deadline = c.DateTime(),
                        Start = c.DateTime(),
                        IsCompleted = c.Boolean(nullable: false),
                        PlannedType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountModels", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("dbo.CostTypeModels", t => t.PlannedType_Id)
                .Index(t => t.AccountId)
                .Index(t => t.PlannedType_Id);
            
            CreateTable(
                "dbo.UserModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        AccessType = c.Int(nullable: false),
                        Account_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountModels", t => t.Account_Id)
                .Index(t => t.Account_Id);
            
            CreateTable(
                "dbo.BadgeModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false, maxLength: 256),
                        BadgeImgUri = c.String(nullable: false, maxLength: 1024),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AccountBadgeModels", "BadgeId", "dbo.BadgeModels");
            DropForeignKey("dbo.AccountBadgeModels", "AccountId", "dbo.AccountModels");
            DropForeignKey("dbo.UserModels", "Account_Id", "dbo.AccountModels");
            DropForeignKey("dbo.PlanModels", "PlannedType_Id", "dbo.CostTypeModels");
            DropForeignKey("dbo.PlanModels", "AccountId", "dbo.AccountModels");
            DropForeignKey("dbo.CostInfoModels", "TypeId", "dbo.CostTypeModels");
            DropForeignKey("dbo.CostInfoModels", "AccountId", "dbo.AccountModels");
            DropIndex("dbo.UserModels", new[] { "Account_Id" });
            DropIndex("dbo.PlanModels", new[] { "PlannedType_Id" });
            DropIndex("dbo.PlanModels", new[] { "AccountId" });
            DropIndex("dbo.CostInfoModels", new[] { "TypeId" });
            DropIndex("dbo.CostInfoModels", new[] { "AccountId" });
            DropIndex("dbo.AccountBadgeModels", new[] { "BadgeId" });
            DropIndex("dbo.AccountBadgeModels", new[] { "AccountId" });
            DropTable("dbo.BadgeModels");
            DropTable("dbo.UserModels");
            DropTable("dbo.PlanModels");
            DropTable("dbo.CostTypeModels");
            DropTable("dbo.CostInfoModels");
            DropTable("dbo.AccountModels");
            DropTable("dbo.AccountBadgeModels");
        }
    }
}
