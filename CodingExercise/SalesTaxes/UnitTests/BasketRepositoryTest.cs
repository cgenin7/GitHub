using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalesTaxesRepositories;
using SalesTaxesModels;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class BasketRepositoryTest
    {
        private static IBasketRepository basketRepository;
        private static IItemRepository itemRepository;
      
        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            basketRepository = RepositoriesFactory.GetBasketRepository();
            itemRepository = RepositoriesFactory.GetItemRepository();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            basketRepository.DeleteAllBasketsWithBasketItems();
            itemRepository.DeleteAllItems();
        }

        [TestMethod]
        public void TestAddBasketWithItems()
        {
            basketRepository.AddBasketWithBasketItems(GetTestBasketWithThreeItems());
            var baskets = basketRepository.GetBaskets();

            Assert.IsTrue(baskets.Count == 1);
            Assert.IsTrue(baskets[0].BasketItems == null || baskets[0].BasketItems.Count == 0);

            var basketsWithItems = basketRepository.GetBasketsWithItems();

            Assert.IsTrue(basketsWithItems.Count == 1);
            Assert.IsTrue(basketsWithItems[0].BasketItems != null);
            Assert.IsTrue(basketsWithItems[0].BasketItems.Count == 3);

            var basket = basketRepository.GetBasket(baskets[0].BasketId);

            Assert.IsTrue(basket.BasketItems == null || basket.BasketItems.Count == 0);

            var basketWithItems = basketRepository.GetBasketWithItems(baskets[0].BasketId);

            Assert.IsTrue(basketWithItems.BasketItems != null);
            Assert.IsTrue(basketWithItems.BasketItems.Count == 3);
        }

        [TestMethod]
        public void TestEditBasket()
        {
            var basket = basketRepository.AddBasketWithBasketItems(GetTestBasketWithThreeItems());
            
            basket.BasketName = "Changed basket name";
            basket.BasketItems[0].Item.ItemName = "Changed item name";
            basket.BasketItems.RemoveAt(2);

            basketRepository.EditBasket(basket);

            basket = basketRepository.GetBasketWithItems(basket.BasketId);

            Assert.IsTrue(string.Compare(basket.BasketName, "Changed basket name") == 0);
            Assert.IsTrue(string.Compare(basket.BasketItems[0].Item.ItemName, "Changed item name") != 0);
            Assert.IsTrue(basket.BasketItems.Count == 3);
        }

        [TestMethod]
        public void TestEditBasketWithItems()
        {
            var basket = basketRepository.AddBasketWithBasketItems(GetTestBasketWithThreeItems());

            var items = itemRepository.GetItems();

            basket.BasketName = "Changed basket name";
            basket.BasketItems.RemoveAt(2);
            basket.BasketItems.Add(new BasketItemModel { BasketId = basket.BasketId, ItemId = items[1].ItemId, ItemQuantity = 1 });

            basketRepository.EditBasketWithBasketItems(basket);

            basket = basketRepository.GetBasketWithItems(basket.BasketId);

            Assert.IsTrue(string.Compare(basket.BasketName, "Changed basket name") == 0);
            Assert.IsTrue(string.Compare(basket.BasketItems[2].Item.ItemName, "music CD") == 0);
            Assert.IsTrue(basket.BasketItems.Count == 3);
        }

        [TestMethod]
        public void TestDeleteBasketWithItems()
        {
            basketRepository.AddBasketWithBasketItems(GetTestBasketWithThreeItems());
            basketRepository.AddBasketWithBasketItems(GetTestBasketWithThreeItems());
            var thirdBasket = basketRepository.AddBasketWithBasketItems(GetTestBasketWithThreeItems());
            
            basketRepository.DeleteBasketWithBasketItems(thirdBasket.BasketId);
            
            var baskets = basketRepository.GetBasketsWithItems();
            Assert.IsTrue(baskets.Count == 2);
        }


        [TestCleanup]
        public void TestCleanup()
        {
        }

        private BasketModel GetTestBasketWithThreeItems()
        {
            return new BasketModel()
            {
                BasketName = "Test basket with 3 items",
                BasketItems =
                    new List<BasketItemModel>
                    {
                        new BasketItemModel() { Item = new ItemModel() { ItemName = "book", ItemPrice = 12.49, Imported = false, CategoryId = 2 }, ItemQuantity = 2 },
                        new BasketItemModel() { Item = new ItemModel() { ItemName = "music CD", ItemPrice = 14.99, Imported = false, CategoryId = 4 }, ItemQuantity = 1 },
                        new BasketItemModel() { Item = new ItemModel() { ItemName = "chocolate bar", ItemPrice = 0.85, Imported = false, CategoryId = 1 }, ItemQuantity = 3 }
                    }
            };
        }
    }
}
