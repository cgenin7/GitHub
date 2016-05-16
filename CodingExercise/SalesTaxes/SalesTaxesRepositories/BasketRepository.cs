using System;
using System.Collections.Generic;
using System.Linq;
using SalesTaxesModels;
using System.Diagnostics;
using System.Data.Entity;
using SalesTaxesData;

namespace SalesTaxesRepositories
{
    public interface IBasketRepository
    {
        IList<BasketModel> GetBaskets();
        IList<BasketModel> GetBasketsWithItems();
        BasketModel GetBasket(int basketId);
        BasketModel GetBasketWithItems(int basketId);
        BasketModel AddBasketWithBasketItems(BasketModel basket);
        void EditBasket(BasketModel basket);
        void EditBasketWithBasketItems(BasketModel basket);
        void DeleteBasketWithBasketItems(int basketId);
        bool DeleteAllBasketsWithBasketItems();
    }

    internal class BasketRepository : IBasketRepository
    {
        public IList<BasketModel> GetBaskets()
        {
            try
            {
                using (var dbContext = new SalesDataDBContext())
                {
                    return dbContext.Baskets.AsNoTracking().ToList();
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("GetBaskets exception: " + ex.Message);
                throw;
            }
        }

        public IList<BasketModel> GetBasketsWithItems()
        {
            try
            {
                using (var dbContext = new SalesDataDBContext())
                {
                    return dbContext.Baskets.Include("BasketItems").Include("BasketItems.Item").Include("BasketItems.Item.Category").AsNoTracking().ToList();
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("GetBasketsWithItems exception: " + ex.Message);
                throw;
            }
        }

        public BasketModel GetBasket(int basketId)
        {
            try
            {
                using (var dbContext = new SalesDataDBContext())
                {
                    return dbContext.Baskets.AsNoTracking().FirstOrDefault(b => b.BasketId == basketId);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("GetBasket exception: " + ex.Message);
                throw;
            }
        }

        public BasketModel GetBasketWithItems(int basketId)
        {
            try
            {
                using (var dbContext = new SalesDataDBContext())
                {
                    return dbContext.Baskets.Include("BasketItems").Include("BasketItems.Item").Include("BasketItems.Item.Category").AsNoTracking().FirstOrDefault(b => b.BasketId == basketId);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("GetBasketWithItems exception: " + ex.Message);
                throw;
            }
        }

        public BasketModel AddBasketWithBasketItems(BasketModel basket)
        {
            try
            {
                using (var dbContext = new SalesDataDBContext())
                {
                    var basketInDb = dbContext.Baskets.Add(basket);
                    dbContext.SaveChanges();
                    return basketInDb;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("AddBasket exception: " + ex.Message);
                throw;
            }
        }

        public void EditBasket(BasketModel basket)
        {
            try
            {
                using (var dbContext = new SalesDataDBContext())
                {
                    dbContext.Entry(basket).State = EntityState.Modified;
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("EditBasket exception: " + ex.Message);
                throw;
            }
        }

        public void EditBasketWithBasketItems(BasketModel basket)
        {
            try
            {
                using (var dbContext = new SalesDataDBContext())
                {
                    var oldBasket = GetBasketWithItems(basket.BasketId);
                    dbContext.Entry(basket).State = EntityState.Modified;

                    foreach (var basketItem in basket.BasketItems)
                    {
                        var oldBasketItem = oldBasket.BasketItems.FirstOrDefault(i => i.ItemId == basketItem.ItemId);
                        if (oldBasketItem != null)
                        {
                            dbContext.Entry(basketItem).State = EntityState.Modified;
                        }
                        else
                        {
                            dbContext.Entry(basketItem).State = EntityState.Added;
                        }
                        oldBasket.BasketItems.Remove(oldBasketItem);
                    }
                    
                    foreach (var basketItem in oldBasket.BasketItems)
                    {
                        var basketItemToRemove = dbContext.BasketItems.Find(basketItem.BasketItemId);
                        dbContext.BasketItems.Remove(basketItemToRemove);
                    }
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("EditBasketWithItem exception: " + ex.Message);
                throw;
            }
        }

        public void DeleteBasketWithBasketItems(int basketId)
        {
            try
            {
                using (var dbContext = new SalesDataDBContext())
                {
                    var basketToRemove = dbContext.Baskets.FirstOrDefault(b => b.BasketId == basketId);

                    if (basketToRemove == null)
                    {
                        Trace.WriteLine("Could not delete basket with id " + basketId + ". It is not in the database");
                    }
                    dbContext.Baskets.Remove(basketToRemove);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("DeleteBasket exception: " + ex.Message);
                throw;
            }
        }

        public bool DeleteAllBasketsWithBasketItems()
        {
            try
            {
                using (var dbContext = new SalesDataDBContext())
                {
                    dbContext.Baskets.RemoveRange(dbContext.Baskets);
                    dbContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("DeleteBaskets exception: " + ex.Message);
                throw;
            }
        }
    }
}
