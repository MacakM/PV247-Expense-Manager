namespace ExpenseManager.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    /// <summary>
    /// Initial migration.
    /// </summary>
    public partial class Initial : DbMigration
    {
        /// <summary>
        /// Performs up.
        /// </summary>
        public override void Up()
        {
            CreateTable(
                "dbo.Badges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 256),
                        BadgeImgUri = c.String(nullable: false, maxLength: 1024),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserBadges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        BadgeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Badges", t => t.BadgeId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.BadgeId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CostInfoUserAccesses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        CostId = c.Int(nullable: false),
                        AccessType = c.Int(nullable: false),
                        CostInfo_Id = c.Int(),
                        Cost_Id = c.Int(),
                        User_Id = c.Int(),
                        User_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CostInfoes", t => t.CostInfo_Id)
                .ForeignKey("dbo.CostInfoes", t => t.Cost_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Users", t => t.User_Id1)
                .Index(t => t.CostInfo_Id)
                .Index(t => t.Cost_Id)
                .Index(t => t.User_Id)
                .Index(t => t.User_Id1);
            
            CreateTable(
                "dbo.CostInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsIncome = c.Boolean(nullable: false),
                        Money = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        TypeId = c.Int(nullable: false),
                        IsPeriodic = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CostTypes", t => t.TypeId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
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
                        UserId = c.Int(nullable: false),
                        Description = c.String(maxLength: 256),
                        PlanType = c.Int(nullable: false),
                        PlannedMoney = c.Int(nullable: false),
                        IsCompleted = c.Boolean(nullable: false),
                        PlannedType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CostTypes", t => t.PlannedType_Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.PlannedType_Id);
            
        }
        
        /// <summary>
        /// Performs down.
        /// </summary>
        public override void Down()
        {
            DropForeignKey("dbo.UserBadges", "UserId", "dbo.Users");
            DropForeignKey("dbo.Plans", "UserId", "dbo.Users");
            DropForeignKey("dbo.Plans", "PlannedType_Id", "dbo.CostTypes");
            DropForeignKey("dbo.CostInfoUserAccesses", "User_Id1", "dbo.Users");
            DropForeignKey("dbo.CostInfoUserAccesses", "User_Id", "dbo.Users");
            DropForeignKey("dbo.CostInfoUserAccesses", "Cost_Id", "dbo.CostInfoes");
            DropForeignKey("dbo.CostInfoes", "UserId", "dbo.Users");
            DropForeignKey("dbo.CostInfoes", "TypeId", "dbo.CostTypes");
            DropForeignKey("dbo.CostInfoUserAccesses", "CostInfo_Id", "dbo.CostInfoes");
            DropForeignKey("dbo.UserBadges", "BadgeId", "dbo.Badges");
            DropIndex("dbo.Plans", new[] { "PlannedType_Id" });
            DropIndex("dbo.Plans", new[] { "UserId" });
            DropIndex("dbo.CostInfoes", new[] { "TypeId" });
            DropIndex("dbo.CostInfoes", new[] { "UserId" });
            DropIndex("dbo.CostInfoUserAccesses", new[] { "User_Id1" });
            DropIndex("dbo.CostInfoUserAccesses", new[] { "User_Id" });
            DropIndex("dbo.CostInfoUserAccesses", new[] { "Cost_Id" });
            DropIndex("dbo.CostInfoUserAccesses", new[] { "CostInfo_Id" });
            DropIndex("dbo.UserBadges", new[] { "BadgeId" });
            DropIndex("dbo.UserBadges", new[] { "UserId" });
            DropTable("dbo.Plans");
            DropTable("dbo.CostTypes");
            DropTable("dbo.CostInfoes");
            DropTable("dbo.CostInfoUserAccesses");
            DropTable("dbo.Users");
            DropTable("dbo.UserBadges");
            DropTable("dbo.Badges");
        }
    }
}
