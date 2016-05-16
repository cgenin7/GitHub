namespace SalesTaxesData
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BasketItems",
                c => new
                    {
                        BasketItemId = c.Int(nullable: false, identity: true),
                        BasketId = c.Int(nullable: false),
                        ItemId = c.Int(nullable: false),
                        ItemQuantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BasketItemId)
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .ForeignKey("dbo.Baskets", t => t.BasketId, cascadeDelete: true)
                .Index(t => t.BasketId)
                .Index(t => t.ItemId);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ItemId = c.Int(nullable: false, identity: true),
                        ItemName = c.String(nullable: false, maxLength: 255),
                        ItemPrice = c.Double(nullable: false),
                        Imported = c.Boolean(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Category = c.Int(nullable: false),
                        PercentageOfBasicTaxes = c.Single(nullable: false),
                        PercentageOfImportTaxes = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Baskets",
                c => new
                    {
                        BasketId = c.Int(nullable: false, identity: true),
                        BasketName = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.BasketId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BasketItems", "BasketId", "dbo.Baskets");
            DropForeignKey("dbo.BasketItems", "ItemId", "dbo.Items");
            DropForeignKey("dbo.Items", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Items", new[] { "CategoryId" });
            DropIndex("dbo.BasketItems", new[] { "ItemId" });
            DropIndex("dbo.BasketItems", new[] { "BasketId" });
            DropTable("dbo.Baskets");
            DropTable("dbo.Categories");
            DropTable("dbo.Items");
            DropTable("dbo.BasketItems");
        }
    }
}
