namespace APMPRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EquipmentModels",
                c => new
                    {
                        EquipmentId = c.Int(nullable: false, identity: true),
                        EquipmentType = c.String(),
                        AdditionalInfo = c.String(),
                    })
                .PrimaryKey(t => t.EquipmentId);
            
            CreateTable(
                "dbo.PersonResponsibleModels",
                c => new
                    {
                        PersonResponsibleId = c.Int(nullable: false, identity: true),
                        LastName = c.String(),
                        FirstName = c.String(),
                        PhoneNb = c.String(),
                    })
                .PrimaryKey(t => t.PersonResponsibleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        FirstName = c.String(maxLength: 255),
                        LastName = c.String(maxLength: 255),
                        PhoneNb = c.String(maxLength: 50),
                        Address = c.String(maxLength: 512),
                        City = c.String(maxLength: 512),
                        State = c.String(maxLength: 50),
                        PostalCode = c.String(maxLength: 50),
                        SkillLevel = c.Int(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.WorkingHoursModels",
                c => new
                    {
                        WorkingHourId = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        StartOfWeek = c.DateTime(nullable: false),
                        MondayHours = c.Single(nullable: false),
                        TuesdayHours = c.Single(nullable: false),
                        WednesdayHours = c.Single(nullable: false),
                        ThursdayHours = c.Single(nullable: false),
                        FridayHours = c.Single(nullable: false),
                        SaturdayHours = c.Single(nullable: false),
                        SundayHours = c.Single(nullable: false),
                        WorkSiteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WorkingHourId)
                .ForeignKey("dbo.WorkSiteModels", t => t.WorkSiteId, cascadeDelete: true)
                .Index(t => t.WorkSiteId);
            
            CreateTable(
                "dbo.WorkSiteModels",
                c => new
                    {
                        WorkSiteId = c.Int(nullable: false, identity: true),
                        Location = c.String(),
                        ContactInfo = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        AdditionalInfo = c.String(),
                    })
                .PrimaryKey(t => t.WorkSiteId);
            
            CreateTable(
                "dbo.WorkSiteEquipmentModels",
                c => new
                    {
                        WorkSiteId = c.Int(nullable: false),
                        EquipmentId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        PersonResponsibleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.WorkSiteId, t.EquipmentId })
                .ForeignKey("dbo.EquipmentModels", t => t.EquipmentId, cascadeDelete: true)
                .ForeignKey("dbo.PersonResponsibleModels", t => t.PersonResponsibleId, cascadeDelete: true)
                .ForeignKey("dbo.WorkSiteModels", t => t.WorkSiteId, cascadeDelete: true)
                .Index(t => t.WorkSiteId)
                .Index(t => t.EquipmentId)
                .Index(t => t.PersonResponsibleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkSiteEquipmentModels", "WorkSiteId", "dbo.WorkSiteModels");
            DropForeignKey("dbo.WorkSiteEquipmentModels", "PersonResponsibleId", "dbo.PersonResponsibleModels");
            DropForeignKey("dbo.WorkSiteEquipmentModels", "EquipmentId", "dbo.EquipmentModels");
            DropForeignKey("dbo.WorkingHoursModels", "WorkSiteId", "dbo.WorkSiteModels");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.WorkSiteEquipmentModels", new[] { "PersonResponsibleId" });
            DropIndex("dbo.WorkSiteEquipmentModels", new[] { "EquipmentId" });
            DropIndex("dbo.WorkSiteEquipmentModels", new[] { "WorkSiteId" });
            DropIndex("dbo.WorkingHoursModels", new[] { "WorkSiteId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.WorkSiteEquipmentModels");
            DropTable("dbo.WorkSiteModels");
            DropTable("dbo.WorkingHoursModels");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.PersonResponsibleModels");
            DropTable("dbo.EquipmentModels");
        }
    }
}
