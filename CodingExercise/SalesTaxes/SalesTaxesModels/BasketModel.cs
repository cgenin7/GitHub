using System.Collections.Generic;

namespace SalesTaxesModels
{
    public class BasketModel
    {
        public BasketModel() { }
        public int BasketId { get; set; }
        public string BasketName { get; set; }

        public IList<BasketItemModel> BasketItems { get; set; }
    }
}
