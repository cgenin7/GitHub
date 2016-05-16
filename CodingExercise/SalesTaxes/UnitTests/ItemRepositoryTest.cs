using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalesTaxesRepositories;
using SalesTaxesModels;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class ItemRepositoryTest
    {

        private static IItemRepository itemRepository;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            itemRepository = RepositoriesFactory.GetItemRepository();

        }

        [TestInitialize]
        public void TestInitialize()
        {
            itemRepository.DeleteAllItems();
        }

        [TestMethod]
        public void TestAddItems()
        {
            itemRepository.AddItem(GetTestItem());
            itemRepository.AddItem(GetTestItem());
            itemRepository.AddItem(GetTestItem());
            
            var items = itemRepository.GetItems();

            Assert.IsTrue(items.Count == 3);
        }

        [TestMethod]
        public void TestEditItem()
        {
            var item = itemRepository.AddItem(GetTestItem());

            item.ItemName = "Changed item name";

            itemRepository.EditItem(item);

            item = itemRepository.GetItem(item.ItemId);

            Assert.IsTrue(string.Compare(item.ItemName, "Changed item name") == 0);
        }

        [TestMethod]
        public void TestDeleteItem()
        {
            itemRepository.AddItem(GetTestItem());
            var item2 = itemRepository.AddItem(GetTestItem());

            itemRepository.DeleteItem(item2.ItemId);

            var items = itemRepository.GetItems();
            Assert.IsTrue(items.Count == 1);
        }


        [TestCleanup]
        public void TestCleanup()
        {
        }

        private ItemModel GetTestItem()
        {
            return new ItemModel() { ItemName = "Item 1", ItemPrice = 12.49, Imported = false, CategoryId = 2 };
        }
    }
}
