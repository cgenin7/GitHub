namespace APMPRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Info : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkingHoursModels", "MondayInfo", c => c.String());
            AddColumn("dbo.WorkingHoursModels", "TuesdayInfo", c => c.String());
            AddColumn("dbo.WorkingHoursModels", "WednesdayInfo", c => c.String());
            AddColumn("dbo.WorkingHoursModels", "ThursdayInfo", c => c.String());
            AddColumn("dbo.WorkingHoursModels", "FridayInfo", c => c.String());
            AddColumn("dbo.WorkingHoursModels", "SaturdayInfo", c => c.String());
            AddColumn("dbo.WorkingHoursModels", "SundayInfo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkingHoursModels", "SundayInfo");
            DropColumn("dbo.WorkingHoursModels", "SaturdayInfo");
            DropColumn("dbo.WorkingHoursModels", "FridayInfo");
            DropColumn("dbo.WorkingHoursModels", "ThursdayInfo");
            DropColumn("dbo.WorkingHoursModels", "WednesdayInfo");
            DropColumn("dbo.WorkingHoursModels", "TuesdayInfo");
            DropColumn("dbo.WorkingHoursModels", "MondayInfo");
        }
    }
}
