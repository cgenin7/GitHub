using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesTaxesModels;

namespace SalesTaxesControllers
{
    public class TaxesCalculationControllerSingleton
    {
        private static TaxesCalculationControllerSingleton m_controller = null;
        private static object m_lockObject = new object();

        private TaxesCalculationControllerSingleton()
        { }

        public static TaxesCalculationControllerSingleton Controller
        {
            get 
            {
                lock (m_lockObject)
                {
                    if (m_controller == null)
                        m_controller = new TaxesCalculationControllerSingleton();
                }
                return m_controller;
            }
        }

        public double CalculateTaxes(ItemModel item)
        {
            var taxes = Math.Round(item.ItemPrice * item.Category.PercentageOfBasicTaxes / 100, 2);
            if (item.Imported)
            {
                taxes += Math.Round(item.ItemPrice * item.Category.PercentageOfImportTaxes / 100, 2);
            }
            return taxes;
        }
    }
}
