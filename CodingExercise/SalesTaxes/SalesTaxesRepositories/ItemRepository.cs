
using SalesTaxesData;
using SalesTaxesModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTaxesRepositories
{
    public interface IItemRepository
    {
        IList<ItemModel> GetItems();
        ItemModel GetItem(int itemId);
        ItemModel AddItem(ItemModel item);
        void EditItem(ItemModel item);
        void DeleteItem(int itemId);
        bool DeleteAllItems();
    }

    public class ItemRepository : IItemRepository
    {
        public IList<ItemModel> GetItems()
        {
            try
            {
                using (var dbContext = new SalesDataDBContext())
                {
                    return dbContext.Items.Include("Category").AsNoTracking().ToList();
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("GetItems exception: " + ex.Message);
                throw;
            }
        }

        public ItemModel GetItem(int itemId)
        {
            try
            {
                using (var dbContext = new SalesDataDBContext())
                {
                    return dbContext.Items.Include("Category").AsNoTracking().FirstOrDefault(i => i.ItemId == itemId);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("GetItem exception: " + ex.Message);
                throw;
            }
        }

        public ItemModel AddItem(ItemModel item)
        {
            try
            {
                using (var dbContext = new SalesDataDBContext())
                {
                    var itemInDb = dbContext.Items.Add(item);
                    dbContext.SaveChanges();
                    return itemInDb;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("AddItem exception: " + ex.Message);
                throw;
            }
        }

        public void EditItem(ItemModel item)
        {
            try
            {
                using (var dbContext = new SalesDataDBContext())
                {
                    dbContext.Entry(item).State = EntityState.Modified;
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("EditItem exception: " + ex.Message);
                throw;
            }
        }

        public void DeleteItem(int itemId)
        {
            try
            {
                using (var dbContext = new SalesDataDBContext())
                {
                    var itemToRemove = dbContext.Items.FirstOrDefault(b => b.ItemId == itemId);

                    if (itemToRemove == null)
                    {
                        Trace.WriteLine("Could not delete item with id " + itemId + ". It is not in the database");
                    }
                    dbContext.Items.Remove(itemToRemove);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("DeleteItem exception: " + ex.Message);
                throw;
            }
        }

        public bool DeleteAllItems()
        {
            try
            {
                using (var dbContext = new SalesDataDBContext())
                {
                    dbContext.Items.RemoveRange(dbContext.Items);
                    dbContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("DeleteAllItems exception: " + ex.Message);
                throw;
            }
        }


    }
}
