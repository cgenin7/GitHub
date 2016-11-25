using System.ComponentModel;

namespace SalesTaxesModels
{
    public class BasketItemModel
    {
        public BasketItemModel()
        {
            ItemQuantity = 1;
        }

        public int BasketItemId { get; set; }

        public int BasketId { get; set; }
        public int ItemId { get; set; }
        public ItemModel Item { get; set; }

        [DisplayName("Quantity")]
        public int ItemQuantity { get; set; }
    }
}
