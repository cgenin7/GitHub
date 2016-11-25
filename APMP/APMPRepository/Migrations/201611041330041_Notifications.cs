namespace APMPRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Notifications : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NotificationModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NotificationEmail = c.String(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NotificationModels", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.NotificationModels", new[] { "UserId" });
            DropTable("dbo.NotificationModels");
        }
    }
}
