
namespace SalesTaxesModels
{
    public class ItemModel
    {
        public ItemModel() { }
        
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public double ItemPrice { get; set; }
        public bool Imported { get; set; }

        public int CategoryId { get; set; }
        public CategoryModel Category { get; set; }
    }
}
