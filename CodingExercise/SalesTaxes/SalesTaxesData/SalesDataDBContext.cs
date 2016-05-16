using SalesTaxesModels;
using System.Data.Entity;

namespace SalesTaxesData
{
    public class SalesDataDBContext : DbContext
    {
        public SalesDataDBContext() : base("DefaultConnection") { } 
        
        public DbSet<BasketModel> Baskets { get; set; }
        public DbSet<ItemModel> Items { get; set; }

        public DbSet<BasketItemModel> BasketItems { get; set; }

        public DbSet<CategoryModel> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new BasketModelMapping());
            modelBuilder.Configurations.Add(new BasketItemModelMapping());
            modelBuilder.Configurations.Add(new ItemModelMapping());
            modelBuilder.Configurations.Add(new CategoryModelMapping());
           
        }

    }
}
