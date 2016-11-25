using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using APMPRepository;
using APMPRepository.Models;

namespace APMPBilling.Controllers
{
    [Authorize]
    public class EquipmentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Equipment
        public ActionResult Index()
        {
            return View(db.Equipments.ToList());
        }

        // GET: Equipment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipmentModel equipmentModel = db.Equipments.Find(id);
            if (equipmentModel == null)
            {
                return HttpNotFound();
            }
            return View(equipmentModel);
        }

        // GET: Equipment/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Equipment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "EquipmentId,EquipmentType,AdditionalInfo")] EquipmentModel equipmentModel)
        {
            if (ModelState.IsValid)
            {
                ModelState.Clear();
                db.Equipments.Add(equipmentModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(equipmentModel);
        }

        // GET: Equipment/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipmentModel equipmentModel = db.Equipments.Find(id);
            if (equipmentModel == null)
            {
                return HttpNotFound();
            }
            return View(equipmentModel);
        }

        // POST: Equipment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "EquipmentId,EquipmentType,AdditionalInfo")] EquipmentModel equipmentModel)
        {
            if (ModelState.IsValid)
            {
                ModelState.Clear();
                db.Entry(equipmentModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(equipmentModel);
        }

        // GET: Equipment/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipmentModel equipmentModel = db.Equipments.Find(id);
            if (equipmentModel == null)
            {
                return HttpNotFound();
            }
            return View(equipmentModel);
        }

        // POST: Equipment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            ModelState.Clear();
            EquipmentModel equipmentModel = db.Equipments.Find(id);
            db.Equipments.Remove(equipmentModel);
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
