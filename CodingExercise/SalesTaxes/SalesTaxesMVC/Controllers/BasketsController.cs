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
    public class BasketsController : Controller
    { 
        private IBasketRepository dbRepo = RepositoriesFactory.GetBasketRepository();

        // GET: Baskets
        public ActionResult Index()
        {
            return View(dbRepo.GetBasketsWithItems());
        }

        // GET: Baskets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BasketModel basketModel = dbRepo.GetBasket((int)id);
            if (basketModel == null)
            {
                return HttpNotFound();
            }
            return View(basketModel);
        }

        // GET: Baskets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Baskets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BasketId,BasketName")] BasketModel basketModel)
        {
            if (ModelState.IsValid)
            {
                dbRepo.AddBasketWithBasketItems(basketModel);
                return RedirectToAction("Index");
            }

            return View(basketModel);
        }

        // GET: Baskets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BasketModel basketModel = dbRepo.GetBasket((int)id);
            if (basketModel == null)
            {
                return HttpNotFound();
            }
            return View(basketModel);
        }

        // POST: Baskets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BasketId,BasketName")] BasketModel basketModel)
        {
            if (ModelState.IsValid)
            {
                dbRepo.EditBasket(basketModel);
                return RedirectToAction("Index");
            }
            return View(basketModel);
        }

        // GET: Baskets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BasketModel basketModel = dbRepo.GetBasket((int)id);
            if (basketModel == null)
            {
                return HttpNotFound();
            }
            return View(basketModel);
        }

        // POST: Baskets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            dbRepo.DeleteBasketWithBasketItems(id);
            return RedirectToAction("Index");
        }
    }
}
