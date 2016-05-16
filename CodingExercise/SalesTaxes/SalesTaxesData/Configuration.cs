using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesTaxesModels;

namespace SalesTaxesData
{
    public sealed class SalesDataConfiguration :
        DbMigrationsConfiguration<SalesDataDBContext>
    {
        public SalesDataConfiguration()
        {
        }

        protected override void Seed(SalesDataDBContext context)
        {
            context.Categories.AddOrUpdate(c => c.Category,
                new[]
                {
                    new CategoryModel() { CategoryId = 0, Category = ECategory.FOOD, PercentageOfBasicTaxes = 0,  PercentageOfImportTaxes = 5 },
                    new CategoryModel() { CategoryId = 1, Category = ECategory.BOOK, PercentageOfBasicTaxes = 0,  PercentageOfImportTaxes = 5 },
                    new CategoryModel() { CategoryId = 2, Category = ECategory.MEDICAL, PercentageOfBasicTaxes = 0,  PercentageOfImportTaxes = 5 },
                    new CategoryModel() { CategoryId = 3, Category = ECategory.OTHER, PercentageOfBasicTaxes = 10,  PercentageOfImportTaxes = 5 }
                });

            context.Items.AddOrUpdate(c =>new { c.ItemName, c.CategoryId },
                new[]
                {
                    new ItemModel() { ItemId = 0, ItemName = "book", ItemPrice = 12.49, Imported = false, CategoryId = 1 },
                    new ItemModel() { ItemId = 1, ItemName = "music CD", ItemPrice = 14.99, Imported = false, CategoryId = 3 },
                    new ItemModel() { ItemId = 2, ItemName = "chocolate bar", ItemPrice = 0.85, Imported = false, CategoryId = 0 },
                    new ItemModel() { ItemId = 3, ItemName = "box of chocolates",  ItemPrice = 10.00, Imported = true, CategoryId = 0 },
                    new ItemModel() { ItemId = 4, ItemName = "bottle of perfume",  ItemPrice = 47.50, Imported = true, CategoryId = 3 },
                    new ItemModel() { ItemId = 5, ItemName = "bottle of perfume",  ItemPrice = 27.99, Imported = true, CategoryId = 3 },
                    new ItemModel() { ItemId = 6, ItemName = "bottle of perfume",  ItemPrice = 18.99, Imported = false, CategoryId = 3 },
                    new ItemModel() { ItemId = 7, ItemName = "packet of headache pills",  ItemPrice = 9.75, Imported = false, CategoryId = 2 },
                    new ItemModel() { ItemId = 8, ItemName = "box of chocolates",  ItemPrice = 11.25, Imported = true, CategoryId = 0 }
                });

            context.Baskets.AddOrUpdate(c => c.BasketName,
                new []
                {
                    new BasketModel() { BasketName = "Basket 1", BasketItems = new List<BasketItemModel>
                                                                        {
                                                                            new BasketItemModel() { ItemId = 0 },
                                                                            new BasketItemModel() { ItemId = 1 },
                                                                            new BasketItemModel() { ItemId = 2 }
                                                                        } },
                    new BasketModel() { BasketName = "Basket 2", BasketItems = new List<BasketItemModel>
                                                                        {
                                                                            new BasketItemModel() { ItemId = 3 },
                                                                            new BasketItemModel() { ItemId = 4 }
                                                                        } },
                    new BasketModel() { BasketName = "Basket 3", BasketItems = new List<BasketItemModel>
                                                                        {
                                                                            new BasketItemModel() { ItemId = 5 },
                                                                            new BasketItemModel() { ItemId = 6 },
                                                                            new BasketItemModel() { ItemId = 7 },
                                                                            new BasketItemModel() { ItemId = 8 }
                                                                        } }
                } );
        
            base.Seed(context);
        }
    }
}
