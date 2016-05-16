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

namespace SalesTaxesMVC.Controllers
{
    public class CategoriesController : Controller
    {
        private SalesDataDBContext db = new SalesDataDBContext();

        // GET: ItemCategory
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        // GET: ItemCategory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryModel itemCategoryModel = db.Categories.Find(id);
            if (itemCategoryModel == null)
            {
                return HttpNotFound();
            }
            return View(itemCategoryModel);
        }

        // GET: ItemCategory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ItemCategory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,Category,PercentageOfBasicTaxes,PercentageOfImportTaxes")] CategoryModel itemCategoryModel)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(itemCategoryModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(itemCategoryModel);
        }

        // GET: ItemCategory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryModel itemCategoryModel = db.Categories.Find(id);
            if (itemCategoryModel == null)
            {
                return HttpNotFound();
            }
            return View(itemCategoryModel);
        }

        // POST: ItemCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryId,Category,PercentageOfBasicTaxes,PercentageOfImportTaxes")] CategoryModel itemCategoryModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemCategoryModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(itemCategoryModel);
        }

        // GET: ItemCategory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryModel itemCategoryModel = db.Categories.Find(id);
            if (itemCategoryModel == null)
            {
                return HttpNotFound();
            }
            return View(itemCategoryModel);
        }

        // POST: ItemCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CategoryModel itemCategoryModel = db.Categories.Find(id);
            db.Categories.Remove(itemCategoryModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
