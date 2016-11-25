
using System.ComponentModel;

namespace SalesTaxesModels
{
    public class CategoryModel
    {
        public CategoryModel() { }

        public int CategoryId { get; set; }
        public ECategory Category { get; set; }

        [DisplayName("% of basic taxes")]
        public float PercentageOfBasicTaxes { get; set; }
        [DisplayName("% of import taxes")]
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
