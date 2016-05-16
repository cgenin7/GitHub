
namespace SalesTaxesModels
{
    public class CategoryModel
    {
        public CategoryModel() { }

        public int CategoryId { get; set; }
        public ECategory Category { get; set; }
        public float PercentageOfBasicTaxes { get; set; }
        public float PercentageOfImportTaxes { get; set; }
    }

    public enum ECategory
    {
        FOOD,
        BOOK,
        MEDICAL,
        OTHER
    }
}
