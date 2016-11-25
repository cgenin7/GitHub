namespace APMPRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserFK : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.WorkingHoursModels", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.WorkingHoursModels", "UserId");
            AddForeignKey("dbo.WorkingHoursModels", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkingHoursModels", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.WorkingHoursModels", new[] { "UserId" });
            AlterColumn("dbo.WorkingHoursModels", "UserId", c => c.String());
        }
    }
}
