using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SalesTaxesData;
using SalesTaxesModels;
using SalesTaxesRepositories;

namespace SalesTaxesMVC.Controllers
{
    public class ItemsController : Controller
    {
        private IItemRepository dbRepo = RepositoriesFactory.GetItemRepository();

        // GET: Items
        public ActionResult Index()
        {
            var items = dbRepo.GetItems();
            return View(items.ToList());
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemModel itemModel = dbRepo.GetItem((int)id);
            if (itemModel == null)
            {
                return HttpNotFound();
            }
            return View(itemModel);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(RepositoriesFactory.GetCategoryRepository().GetCategories(), "CategoryId", "CategoryId");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemId,ItemName,ItemPrice,Imported,CategoryId")] ItemModel itemModel)
        {
            if (ModelState.IsValid)
            {
                dbRepo.AddItem(itemModel);
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(RepositoriesFactory.GetCategoryRepository().GetCategories(), "CategoryId", "CategoryId", itemModel.CategoryId);
            return View(itemModel);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemModel itemModel = dbRepo.GetItem((int)id);
            if (itemModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(RepositoriesFactory.GetCategoryRepository().GetCategories(), "CategoryId", "CategoryId", itemModel.CategoryId);
            return View(itemModel);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemId,ItemName,ItemPrice,Imported,CategoryId")] ItemModel itemModel)
        {
            if (ModelState.IsValid)
            {
                dbRepo.EditItem(itemModel);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(RepositoriesFactory.GetCategoryRepository().GetCategories(), "CategoryId", "CategoryId", itemModel.CategoryId);
            return View(itemModel);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemModel itemModel = dbRepo.GetItem((int)id);
            if (itemModel == null)
            {
                return HttpNotFound();
            }
            return View(itemModel);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            dbRepo.DeleteItem(id);
            return RedirectToAction("Index");
        }
    }
}
