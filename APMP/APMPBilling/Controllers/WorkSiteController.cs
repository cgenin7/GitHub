using System.Net;
using System.Web.Mvc;
using APMPRepository;
using APMPRepository.Models;
using APMPBilling.ViewModels;
using System.Threading.Tasks;

namespace APMPBilling.Controllers
{
    [Authorize]
    public class WorkSiteController : Controller
    {
        private WorkSiteRepository _workSiteRepo = new WorkSiteRepository();

        // GET: WorkSite
        public async Task<ActionResult> Index()
        {
            return View(await _workSiteRepo.GetWorkSitesAsync());
        }

        // GET: WorkSite/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkSiteModel workSiteModel = await _workSiteRepo.GetWorkSiteAsync((int)id);
            if (workSiteModel == null)
            {
                return HttpNotFound();
            }
            return View(workSiteModel);
        }

        // GET: WorkSite/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            WorkSiteViewModel model = new WorkSiteViewModel();

            return View(model);
        }

        // POST: WorkSite/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create([Bind(Include = "WorkSite,ActiveState")] WorkSiteViewModel workSiteModel)
        {
            if (ModelState.IsValid)
            {
                ModelState.Clear();
                workSiteModel.WorkSite.IsActive = workSiteModel.ActiveState;
                await _workSiteRepo.AddWorkSiteAsync(workSiteModel.WorkSite);
                return RedirectToAction("Index");
            }

            return View(workSiteModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisplayPartialWorkSite(WorkSiteModel workSiteModel)
        {
            if (ModelState.IsValid)
            {
                ModelState.Clear();
                var workSite = await _workSiteRepo.GetWorkSiteAsync(workSiteModel.WorkSiteId);
                if (workSite != null)
                {
                    workSite.IsActive = !workSite.IsActive;
                    await _workSiteRepo.SaveChangesAsync();

                    ModelState.Clear();
                    return PartialView("_WorkSiteIndex", workSite);
                }
            }
            return PartialView("_WorkSiteIndex", workSiteModel);
        }

        // GET: WorkSite/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            WorkSiteViewModel model = new WorkSiteViewModel();
            model.WorkSite = await _workSiteRepo.GetWorkSiteAsync((int)id);

            if (model.WorkSite == null)
            {
                return HttpNotFound();
            }
            model.ActiveState = model.WorkSite.IsActive;
            
            return View(model);
        }

        // POST: WorkSite/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit([Bind(Include = "WorkSite,ActiveState")] WorkSiteViewModel workSiteModel)
        {
            if (ModelState.IsValid)
            {
                ModelState.Clear();
                workSiteModel.WorkSite.IsActive = workSiteModel.ActiveState;
                await _workSiteRepo.EditWorkSiteAsync(workSiteModel.WorkSite);
                return RedirectToAction("Index");
            }
            return View(workSiteModel);
        }

        // GET: WorkSite/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkSiteModel workSiteModel = await _workSiteRepo.GetWorkSiteAsync((int)id);
            if (workSiteModel == null)
            {
                return HttpNotFound();
            }
            return View(workSiteModel);
        }

        // POST: WorkSite/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ModelState.Clear();
            await _workSiteRepo.RemoveWorkSiteAsync((int)id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _workSiteRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
