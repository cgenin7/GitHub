namespace APMPRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Disabled : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Disabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.WorkSiteModels", "Disabled", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkSiteModels", "Disabled");
            DropColumn("dbo.AspNetUsers", "Disabled");
        }
    }
}
