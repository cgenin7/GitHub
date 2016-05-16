using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesTaxesControllers;
using SalesTaxesModels;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            InitializeTestData();
            DisplayBaskets();
            Console.ReadKey();
        }

        private static void InitializeTestData()
        {
            /*var basket = new BasketModel() 
            { 
                BasketName = "1", 
                Items =  
                    new List<BasketItemModel>
                    {
                        new ItemModel() { ItemName = "book",  ItemPrice = 12.49, Imported = false, Category = EItemCategory.BOOK },
                        new ItemModel() { ItemName = "music CD",  ItemPrice = 14.99, Imported = false, Category = EItemCategory.OTHER   },
                        new ItemModel() { ItemName = "chocolate bar",  ItemPrice = 0.85, Imported = false, Category = EItemCategory.FOOD  }
                    }
                };
            //if (!BasketsControllerSingleton.Controller.AddBasket(basket))
            //    Console.WriteLine("Could not add basket in database");

            basket = new BasketModel()
            {
                BasketName = "2",
                Items = 
                    new List<ItemModel>
                    {
                        new ItemModel() { ItemName = "box of chocolates",  ItemPrice = 10.00, Imported = true, Category = EItemCategory.FOOD  },
                        new ItemModel() { ItemName = "bottle of perfume",  ItemPrice = 47.50, Imported = true, Category = EItemCategory.OTHER  }
                    }
                };
            //if (!BasketsControllerSingleton.Controller.AddBasket(basket))
            //    Console.WriteLine("Could not add basket in database");

            basket = new BasketModel()
            {
                BasketName = "3",
                Items = 
                        new List<ItemModel>
                        {
                            new ItemModel() { ItemId = 5, ItemName = "bottle of perfume",  ItemPrice = 27.99, Imported = true, Category = EItemCategory.OTHER , BasketId = 2  },
                            new ItemModel() { ItemId = 6, ItemName = "bottle of perfume",  ItemPrice = 18.99, Imported = false, Category = EItemCategory.OTHER , BasketId = 2  },
                            new ItemModel() { ItemId = 7, ItemName = "packet of headache pills",  ItemPrice = 9.75, Imported = false, Category = EItemCategory.MEDICAL , BasketId = 2  },
                            new ItemModel() { ItemId = 8, ItemName = "box of chocolates",  ItemPrice = 11.25, Imported = true, Category = EItemCategory.FOOD , BasketId = 2  }
                        }
                    };
            //if (!BasketsControllerSingleton.Controller.AddBasket(basket))
            //    Console.WriteLine("Could not add basket in database");*/
        }

        private static void DisplayBaskets()
        {
            /*List<BasketModel> baskets = null; // CGe  BasketsControllerSingleton.Controller.GetBaskets();

            if (baskets != null)
            {
                foreach (BasketModel basket in baskets)
                {
                    DisplayBasket(basket);

                    var totalTaxes = 0.0;
                    var totalPrice = 0.0;
                    foreach (var item in basket.BasketItems)
                    {
                        double taxes = TaxesCalculationControllerSingleton.Controller.CalculateTaxes(item);
                        DisplayItem(item, taxes);

                        totalTaxes += taxes;
                        totalPrice += (item.ItemPrice + taxes);
                    }
                    DisplayTotal(totalTaxes, totalPrice);
                }
            }*/

        }

        private static void DisplayBasket(BasketModel basket)
        {
            Console.WriteLine("Receipt " + basket.BasketName);
        }

        private static void DisplayItem(BasketItemModel basketItem, double taxes)
        {
            var item = basketItem.Item;

            double price = basketItem.ItemQuantity * (item.ItemPrice + taxes);

            Console.WriteLine("     " + basketItem.ItemQuantity + " " + (item.Imported ? "imported " : "") + item.ItemName +
                ": " + FormatCurrency(price));
        }

        private static void DisplayTotal(double totalTaxes, double totalPrice)
        {
            Console.WriteLine("     Sales Taxes: " + FormatCurrency(totalTaxes));
            Console.WriteLine("     Total: " + FormatCurrency(totalPrice));
        }

        private static string FormatCurrency(double value)
        {
            return value.ToString("C");
        }
    }
}
