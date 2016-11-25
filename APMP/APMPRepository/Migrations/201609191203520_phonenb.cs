namespace APMPRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class phonenb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkSiteModels", "PhoneNb", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkSiteModels", "PhoneNb");
        }
    }
}
