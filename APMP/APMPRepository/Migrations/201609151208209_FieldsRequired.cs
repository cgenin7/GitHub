namespace APMPRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FieldsRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.AspNetUsers", "PhoneNb", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.AspNetUsers", "Address", c => c.String(nullable: false, maxLength: 512));
            AlterColumn("dbo.AspNetUsers", "City", c => c.String(nullable: false, maxLength: 512));
            AlterColumn("dbo.AspNetUsers", "State", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.AspNetUsers", "PostalCode", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "PostalCode", c => c.String(maxLength: 50));
            AlterColumn("dbo.AspNetUsers", "State", c => c.String(maxLength: 50));
            AlterColumn("dbo.AspNetUsers", "City", c => c.String(maxLength: 512));
            AlterColumn("dbo.AspNetUsers", "Address", c => c.String(maxLength: 512));
            AlterColumn("dbo.AspNetUsers", "PhoneNb", c => c.String(maxLength: 50));
            AlterColumn("dbo.AspNetUsers", "LastName", c => c.String(maxLength: 255));
            AlterColumn("dbo.AspNetUsers", "FirstName", c => c.String(maxLength: 255));
        }
    }
}
