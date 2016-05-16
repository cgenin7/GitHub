using SalesTaxesModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace SalesTaxesData
{
    public class BasketModelMapping : EntityTypeConfiguration<BasketModel>
    {
        public BasketModelMapping()
        {
            HasKey(t => t.BasketId);

            Property(t => t.BasketName)
                .IsRequired().HasMaxLength(255);

            ToTable("Baskets");
        }
    }

    public class BasketItemModelMapping : EntityTypeConfiguration<BasketItemModel>
    {
        public BasketItemModelMapping()
        {
            HasKey(t => t.BasketItemId);

            ToTable("BasketItems");
        }
    }

    public class CategoryModelMapping : EntityTypeConfiguration<CategoryModel>
    {
        public CategoryModelMapping()
        {
            HasKey(t => t.CategoryId);

            Property(t => t.Category)
                .IsRequired();

            ToTable("Categories");
        }
    }

    public class ItemModelMapping : EntityTypeConfiguration<ItemModel>
    {
        public ItemModelMapping()
        {
            HasKey(t => t.ItemId);

            Property(t => t.ItemName)
                .IsRequired().HasMaxLength(255);

            Property(t => t.ItemPrice)
                .IsRequired();

            ToTable("Items");
        }
    }
}
