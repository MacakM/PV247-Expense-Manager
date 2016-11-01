namespace ExpenseManager.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountBadges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountId = c.Int(nullable: false),
                        BadgeId = c.Int(nullable: false),
                        Achieved = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("dbo.Badges", t => t.BadgeId, cascadeDelete: true)
                .Index(t => t.AccountId)
                .Index(t => t.BadgeId);
            
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CostInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsIncome = c.Boolean(nullable: false),
                        Money = c.Int(nullable: false),
                        AccountId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        TypeId = c.Int(nullable: false),
                        IsPeriodic = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("dbo.CostTypes", t => t.TypeId, cascadeDelete: true)
                .Index(t => t.AccountId)
                .Index(t => t.TypeId);
            
            CreateTable(
                "dbo.CostTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Plans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountId = c.Int(nullable: false),
                        Description = c.String(maxLength: 256),
                        PlanType = c.Int(nullable: false),
                        PlannedMoney = c.Int(nullable: false),
                        Deadline = c.DateTime(),
                        IsCompleted = c.Boolean(nullable: false),
                        PlannedType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("dbo.CostTypes", t => t.PlannedType_Id)
                .Index(t => t.AccountId)
                .Index(t => t.PlannedType_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        AccessType = c.Int(nullable: false),
                        Account_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Account_Id, cascadeDelete: true)
                .Index(t => t.Account_Id);
            
            CreateTable(
                "dbo.Badges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 256),
                        BadgeImgUri = c.String(nullable: false, maxLength: 1024),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AccountBadges", "BadgeId", "dbo.Badges");
            DropForeignKey("dbo.AccountBadges", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.Users", "Account_Id", "dbo.Accounts");
            DropForeignKey("dbo.Plans", "PlannedType_Id", "dbo.CostTypes");
            DropForeignKey("dbo.Plans", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.CostInfoes", "TypeId", "dbo.CostTypes");
            DropForeignKey("dbo.CostInfoes", "AccountId", "dbo.Accounts");
            DropIndex("dbo.Users", new[] { "Account_Id" });
            DropIndex("dbo.Plans", new[] { "PlannedType_Id" });
            DropIndex("dbo.Plans", new[] { "AccountId" });
            DropIndex("dbo.CostInfoes", new[] { "TypeId" });
            DropIndex("dbo.CostInfoes", new[] { "AccountId" });
            DropIndex("dbo.AccountBadges", new[] { "BadgeId" });
            DropIndex("dbo.AccountBadges", new[] { "AccountId" });
            DropTable("dbo.Badges");
            DropTable("dbo.Users");
            DropTable("dbo.Plans");
            DropTable("dbo.CostTypes");
            DropTable("dbo.CostInfoes");
            DropTable("dbo.Accounts");
            DropTable("dbo.AccountBadges");
        }
    }
}
