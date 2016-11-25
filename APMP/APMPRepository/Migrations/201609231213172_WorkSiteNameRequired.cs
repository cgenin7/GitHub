namespace APMPRepository.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class WorkSiteNameRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.WorkSiteModels", "Location", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.WorkSiteModels", "Location", c => c.String());
        }
    }
}
