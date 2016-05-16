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
    public interface ICategoryRepository
    {
        IList<CategoryModel> GetCategories();
        CategoryModel GetCategory(int categoryId);
        CategoryModel AddCategory(CategoryModel category);
        void EditCategory(CategoryModel category);
        void DeleteCategory(int categoryId);
        bool DeleteAllCategories();
    }

    internal class CategoryRepository : ICategoryRepository
    {
        public IList<CategoryModel> GetCategories()
        {
            try
            {
                using (var dbContext = new SalesDataDBContext())
                {
                    return dbContext.Categories.AsNoTracking().ToList();
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("GetCategories exception: " + ex.Message);
                throw;
            }
        }

        public CategoryModel GetCategory(int categoryId)
        {
            try
            {
                using (var dbContext = new SalesDataDBContext())
                {
                    return dbContext.Categories.AsNoTracking().FirstOrDefault(i => i.CategoryId == categoryId);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("GetCategory exception: " + ex.Message);
                throw;
            }
        }

        public CategoryModel AddCategory(CategoryModel category)
        {
            try
            {
                using (var dbContext = new SalesDataDBContext())
                {
                    var categoryInDb = dbContext.Categories.Add(category);
                    dbContext.SaveChanges();
                    return categoryInDb;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("AddCategory exception: " + ex.Message);
                throw;
            }
        }

        public void EditCategory(CategoryModel category)
        {
            try
            {
                using (var dbContext = new SalesDataDBContext())
                {
                    dbContext.Entry(category).State = EntityState.Modified;
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("EditCategory exception: " + ex.Message);
                throw;
            }
        }

        public void DeleteCategory(int categoryId)
        {
            try
            {
                using (var dbContext = new SalesDataDBContext())
                {
                    var categoryToRemove = dbContext.Categories.FirstOrDefault(b => b.CategoryId == categoryId);

                    if (categoryToRemove == null)
                    {
                        Trace.WriteLine("Could not delete category with id " + categoryId + ". It is not in the database");
                    }
                    dbContext.Categories.Remove(categoryToRemove);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("DeleteCategory exception: " + ex.Message);
                throw;
            }
        }

        public bool DeleteAllCategories()
        {
            try
            {
                using (var dbContext = new SalesDataDBContext())
                {
                    dbContext.Categories.RemoveRange(dbContext.Categories);
                    dbContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("DeleteAllCategories exception: " + ex.Message);
                throw;
            }
        }

    }
}
