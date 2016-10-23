namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
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
                "dbo.CostInfoPastes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CostInfoId = c.Int(nullable: false),
                        PasteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CostInfoes", t => t.CostInfoId, cascadeDelete: true)
                .ForeignKey("dbo.Pastes", t => t.PasteId, cascadeDelete: true)
                .Index(t => t.CostInfoId)
                .Index(t => t.PasteId);
            
            CreateTable(
                "dbo.Pastes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Guid = c.String(nullable: false),
                        OwnerId = c.Int(),
                        Expiration = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.OwnerId)
                .Index(t => t.OwnerId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
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
                "dbo.Badges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 256),
                        BadgeImgUri = c.String(nullable: false, maxLength: 1024),
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
            
            CreateTable(
                "dbo.CostTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserPasteAccesses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        PasteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pastes", t => t.PasteId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.PasteId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CostInfoes", "UserId", "dbo.Users");
            DropForeignKey("dbo.CostInfoes", "TypeId", "dbo.CostTypes");
            DropForeignKey("dbo.CostInfoPastes", "PasteId", "dbo.Pastes");
            DropForeignKey("dbo.UserPasteAccesses", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserPasteAccesses", "PasteId", "dbo.Pastes");
            DropForeignKey("dbo.Plans", "UserId", "dbo.Users");
            DropForeignKey("dbo.Plans", "PlannedType_Id", "dbo.CostTypes");
            DropForeignKey("dbo.Pastes", "OwnerId", "dbo.Users");
            DropForeignKey("dbo.UserBadges", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserBadges", "BadgeId", "dbo.Badges");
            DropForeignKey("dbo.CostInfoPastes", "CostInfoId", "dbo.CostInfoes");
            DropIndex("dbo.UserPasteAccesses", new[] { "PasteId" });
            DropIndex("dbo.UserPasteAccesses", new[] { "UserId" });
            DropIndex("dbo.Plans", new[] { "PlannedType_Id" });
            DropIndex("dbo.Plans", new[] { "UserId" });
            DropIndex("dbo.UserBadges", new[] { "BadgeId" });
            DropIndex("dbo.UserBadges", new[] { "UserId" });
            DropIndex("dbo.Pastes", new[] { "OwnerId" });
            DropIndex("dbo.CostInfoPastes", new[] { "PasteId" });
            DropIndex("dbo.CostInfoPastes", new[] { "CostInfoId" });
            DropIndex("dbo.CostInfoes", new[] { "TypeId" });
            DropIndex("dbo.CostInfoes", new[] { "UserId" });
            DropTable("dbo.UserPasteAccesses");
            DropTable("dbo.CostTypes");
            DropTable("dbo.Plans");
            DropTable("dbo.Badges");
            DropTable("dbo.UserBadges");
            DropTable("dbo.Users");
            DropTable("dbo.Pastes");
            DropTable("dbo.CostInfoPastes");
            DropTable("dbo.CostInfoes");
        }
    }
}
