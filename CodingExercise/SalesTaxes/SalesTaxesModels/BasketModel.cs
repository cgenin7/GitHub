using System.Collections.Generic;
using System.ComponentModel;

namespace SalesTaxesModels
{
    public class BasketModel
    {
        public BasketModel() { }
        public int BasketId { get; set; }

        [DisplayName("Name")]
        public string BasketName { get; set; }

        public IList<BasketItemModel> BasketItems { get; set; }
    }
}
