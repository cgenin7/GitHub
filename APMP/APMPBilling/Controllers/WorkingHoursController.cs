using System.Linq;
using System.Web.Mvc;
using APMPRepository;
using APMPRepository.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using APMPBilling.ViewModels;
using System.Net;
using System.Threading.Tasks;
using log4net;

namespace APMPBilling.Controllers
{
    [Authorize]
    public class WorkingHoursController : Controller
    {
        private WorkingHoursRepository _workingHoursRepo = new WorkingHoursRepository();
        private UserRepository _userRepo = new UserRepository();
        private WorkSiteRepository _workSiteRepo = new WorkSiteRepository();
   
        // GET: WorkHoursViewModel
        public async Task<ActionResult> Index()
        {
            return View(await GetHours(DateTime.Now.Date));
        }

        
        // GET: WorkHoursListViewModel
        public async Task<ActionResult> ListHours(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                userId = User.Identity.GetUserId();
            }
            DateTime startOfWeek = DateTime.Now.StartOfWeek(DayOfWeek.Monday).Date;

            return View(await GetHoursList(userId, startOfWeek));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(WorkHoursViewModel workHoursView)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                ModelState.Clear();
                var dbWorkingHour = await _workingHoursRepo.GetWorkingHoursAsync(workHoursView.UserId, workHoursView.SelectedSiteId,
                    workHoursView.DisplayDay.StartOfWeek(DayOfWeek.Monday));

                if (dbWorkingHour != null)
                {
                    AddHours(dbWorkingHour, workHoursView);
                    if (HasHours(dbWorkingHour))
                        await _workingHoursRepo.SaveChangesAsync();
                    else
                        await _workingHoursRepo.RemoveWorkingHoursAsync(dbWorkingHour.UserId, dbWorkingHour.WorkSiteId, dbWorkingHour.StartOfWeek);
                }
                else
                {
                    var workingHours = GetWorkingHoursModel(workHoursView);
                    AddHours(workingHours, workHoursView);

                    if (HasHours(workingHours))
                        await _workingHoursRepo.AddWorkingHoursAsync(workingHours);
                }
                workHoursView = await GetHours(workHoursView.DisplayDay, workHoursView.SelectedSiteId);
                
                await SettingsController.NotifyHoursChanged(userId, workHoursView);
                
                return View("Index", workHoursView);
            }

            return View("Index", workHoursView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeWorkSite(WorkHoursViewModel workingHoursView)
        {
            if (ModelState.IsValid)
            {
                ModelState.Clear();
                var hours = await GetHours(workingHoursView.DisplayDay, workingHoursView.SelectedSiteId);
                return PartialView("_Index", hours);
            }
            var hoursView = await GetHours(workingHoursView.DisplayDay);
            return PartialView("_Index", hoursView);
        }

         // POST: WorkingHours/ListHours
         // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
         // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
         [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ListHours(WorkHoursListViewModel workingHoursView)
        {
            if (ModelState.IsValid)
            {
                ModelState.Clear();
                foreach (var worksiteHours in workingHoursView.WorksiteHours)
                {
                    var dbWorkingHour = await _workingHoursRepo.GetWorkingHoursAsync(worksiteHours.UserId,
                        worksiteHours.WorkSiteId, worksiteHours.StartOfWeek);

                    if (dbWorkingHour != null)
                    {
                        AddHours(dbWorkingHour, worksiteHours);
                        await _workingHoursRepo.SaveChangesAsync();
                    }
                    else
                    {
                        await _workingHoursRepo.AddWorkingHoursAsync(worksiteHours);
                    }
                }
                
                return View("ListHours", await GetHoursList(workingHoursView.UserId, workingHoursView.StartOfWeek));
            }

            return View("ListHours", workingHoursView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeUser(WorkHoursListViewModel workingHoursView)
        {
            if (ModelState.IsValid)
            {
                ModelState.Clear();
                return View(await GetHoursList(workingHoursView.Users.SelectedValue as string, workingHoursView.StartOfWeek));
            }
            return View(workingHoursView);
        }

        // GET: WorkingHours/ChangeDay
        public async Task<ActionResult> ChangeDay(string currentDay, int workSiteId, int shift = 0)
        {
            if (currentDay == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModelState.Clear();
            return View("Index", await GetHours(DateTime.ParseExact(currentDay, "ddMMyyyy", null).AddDays(shift), workSiteId));
        }

        // GET: WorkingHours/ChangeWeekList
        public async Task<ActionResult> ChangeWeekList(string UserId, string startOfWeek, int shift)
        {
            if (startOfWeek == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModelState.Clear();
            return View("ListHours", await GetHoursList(UserId, DateTime.ParseExact(startOfWeek, "ddMMyyyy", null).AddDays(shift)));
        }
        
        // POST: WorkingHoursModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "WorkingHourId,UserId,WorkDay,NbHours,WorkSiteId")] WorkingHoursModel workingHoursModel)
        {
            if (ModelState.IsValid)
            {
                ModelState.Clear();
                await _workingHoursRepo.AddWorkingHoursAsync(workingHoursModel);
                
                return RedirectToAction("Index");
            }

            return View(workingHoursModel);
        }

        private void AddHours(WorkingHoursModel dbModel, WorkingHoursModel viewModel)
        {
            dbModel.MondayHours = viewModel.MondayHours;
            dbModel.TuesdayHours = viewModel.TuesdayHours;
            dbModel.WednesdayHours = viewModel.WednesdayHours;
            dbModel.ThursdayHours = viewModel.ThursdayHours;
            dbModel.FridayHours = viewModel.FridayHours;
            dbModel.SaturdayHours = viewModel.SaturdayHours;
            dbModel.SundayHours = viewModel.SundayHours;
            dbModel.MondayInfo = viewModel.MondayInfo;
            dbModel.TuesdayInfo = viewModel.TuesdayInfo;
            dbModel.WednesdayInfo = viewModel.WednesdayInfo;
            dbModel.ThursdayInfo = viewModel.ThursdayInfo;
            dbModel.FridayInfo = viewModel.FridayInfo;
            dbModel.SaturdayInfo = viewModel.SaturdayInfo;
            dbModel.SundayInfo = viewModel.SundayInfo;
        }

        private void AddHours(WorkingHoursModel dbModel, WorkHoursViewModel viewModel)
        {
            switch (viewModel.DisplayDay.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    dbModel.MondayHours = viewModel.NbHours;
                    dbModel.MondayInfo = viewModel.Info;
                    break;
                case DayOfWeek.Tuesday:
                    dbModel.TuesdayHours = viewModel.NbHours;
                    dbModel.TuesdayInfo = viewModel.Info;
                    break;
                case DayOfWeek.Wednesday:
                    dbModel.WednesdayHours = viewModel.NbHours;
                    dbModel.WednesdayInfo = viewModel.Info;
                    break;
                case DayOfWeek.Thursday:
                    dbModel.ThursdayHours = viewModel.NbHours;
                    dbModel.ThursdayInfo = viewModel.Info;
                    break;
                case DayOfWeek.Friday:
                    dbModel.FridayHours = viewModel.NbHours;
                    dbModel.FridayInfo = viewModel.Info;
                    break;
                case DayOfWeek.Saturday:
                    dbModel.SaturdayHours = viewModel.NbHours;
                    dbModel.SaturdayInfo = viewModel.Info;
                    break;
                case DayOfWeek.Sunday:
                    dbModel.SundayHours = viewModel.NbHours;
                    dbModel.SundayInfo = viewModel.Info;
                    break;
            }
        }

        private bool HasHours(WorkingHoursModel workingHours)
        {
            if (workingHours == null)
            {
                return false;
            }
            return (workingHours.MondayHours + workingHours.TuesdayHours + workingHours.WednesdayHours +
                workingHours.ThursdayHours + workingHours.FridayHours + workingHours.SaturdayHours + workingHours.SundayHours) > 0 ||
                !string.IsNullOrEmpty(workingHours.MondayInfo) || !string.IsNullOrEmpty(workingHours.TuesdayInfo) || !string.IsNullOrEmpty(workingHours.WednesdayInfo) ||
                !string.IsNullOrEmpty(workingHours.ThursdayInfo) || !string.IsNullOrEmpty(workingHours.FridayInfo) || !string.IsNullOrEmpty(workingHours.SaturdayInfo) ||
                !string.IsNullOrEmpty(workingHours.SundayInfo);
        }

        private async Task<WorkHoursViewModel> GetHours(DateTime displayDate, int? workSiteId = null)
        {
            var userId = User.Identity.GetUserId();
            var user = await _userRepo.GetUserAsync(userId);
            var workSites = await GetWorkSites(userId, displayDate);

            var workHoursViewModel = new WorkHoursViewModel(user.FirstName + " " + user.LastName, user.Id, displayDate);
            workHoursViewModel.Month = FormatterHelper.FormatMonth(displayDate);
           
            if (workSites.Count > 0)
            {
                foreach (var site in workSites)
                {
                    var workHours = await GetWorkHours(userId, displayDate.Date, site.WorkSiteId);

                    GetWorkHourViewModel(user, displayDate, workHours, ref workHoursViewModel);

                    workHoursViewModel.Sites = new SelectList(workSites, "WorkSiteId", "Location", workSites.FirstOrDefault().WorkSiteId);
                    if (workSiteId == null || workSiteId == site.WorkSiteId)
                    {
                        workHoursViewModel.SelectedSiteId = site.WorkSiteId;
                        workHoursViewModel.SiteName = site.Location;
                        break;
                    }
                    workHoursViewModel.SelectedSiteId = site.WorkSiteId;
                }
            }
            return workHoursViewModel;
        }

        private WorkingHoursModel GetWorkingHoursModel(WorkHoursViewModel workingHoursView)
        {
            var model = new WorkingHoursModel();
            model.StartOfWeek = workingHoursView.DisplayDay.StartOfWeek(DayOfWeek.Monday);
            model.UserId = workingHoursView.UserId;
            model.WorkSiteId = workingHoursView.SelectedSiteId;

            return model;
        }

        private async Task<WorkHoursListViewModel> GetHoursList(string userId, DateTime startOfWeek)
        {
            var user = await _userRepo.GetUserAsync(userId);
            WorkHoursListViewModel workingHoursView = new WorkHoursListViewModel(userId, user.FirstName + " " + user.LastName, startOfWeek);
            workingHoursView.Period = FormatterHelper.FormatPeriod(startOfWeek, startOfWeek.AddDays(6));
            
            if (User.IsInRole("Admin"))
            {
                workingHoursView.Users = new SelectList(await _userRepo.GetUsersAsync());
            }
            List<WorkSiteModel> workSites = await _workSiteRepo.GetWorkSitesAsync();

            foreach (var workSite in workSites)
            {
                var workingHours = await _workingHoursRepo.GetWorkingHoursWithWorkSiteAsync(workSite.WorkSiteId,
                        userId, startOfWeek);

                if (workingHours == null)
                {
                    workingHours = new WorkingHoursModel()
                    {
                        UserId = userId,
                        WorkSite = workSite,
                        WorkSiteId = workSite.WorkSiteId,
                        StartOfWeek = startOfWeek
                    };
                }
                if (workSite.IsActive || HasHours(workingHours))
                 {
                    workingHoursView.WorksiteHours.Add(workingHours);
                }
            }
            return workingHoursView;
        }

        private async Task<List<WorkSiteModel>> GetWorkSites(string userId, DateTime displayDate)
        {
            List<WorkSiteModel> workSites = await _workSiteRepo.GetWorkSitesAsync();
            List<WorkSiteModel> activeWorkSites = new List<WorkSiteModel>();

            foreach (var workSite in workSites)
            {
                if (!workSite.IsActive)
                {
                    var workingHours = await _workingHoursRepo.GetWorkingHoursWithWorkSiteAsync(workSite.WorkSiteId, userId,
                            displayDate.StartOfWeek(DayOfWeek.Monday));

                    if (workingHours != null && HasHours(workingHours))
                    {
                        activeWorkSites.Add(workSite);
                    }
                }
                else
                {
                    activeWorkSites.Add(workSite);
                }
            }
            return activeWorkSites;
        }

        private async Task<WorkingHoursModel> GetWorkHours(string userId, DateTime displayDate, int workSiteId)
        {
            DateTime startOfWeek = displayDate.StartOfWeek(DayOfWeek.Monday);
            WorkingHoursModel model = new WorkingHoursModel();
            var workHours = await _workingHoursRepo.GetWorkingHoursWithWorkSiteAsync(workSiteId, userId,
                            startOfWeek);

            if (workHours == null)
            {
                workHours = new WorkingHoursModel() {
                    UserId = userId,
                    StartOfWeek = startOfWeek,
                    WorkSiteId = workSiteId};
            }
            return workHours;
        }

        private void GetWorkHourViewModel(ApplicationUser user, DateTime displayDate, WorkingHoursModel workHours, ref WorkHoursViewModel workHoursViewModel)
        {
            switch (displayDate.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    workHoursViewModel.NbHours = workHours.MondayHours;
                    workHoursViewModel.Info = workHours.MondayInfo;
                    break;
                case DayOfWeek.Tuesday:
                    workHoursViewModel.NbHours = workHours.TuesdayHours;
                    workHoursViewModel.Info = workHours.TuesdayInfo;
                    break;
                case DayOfWeek.Wednesday:
                    workHoursViewModel.NbHours = workHours.WednesdayHours;
                    workHoursViewModel.Info = workHours.WednesdayInfo;
                    break;
                case DayOfWeek.Thursday:
                    workHoursViewModel.NbHours = workHours.ThursdayHours;
                    workHoursViewModel.Info = workHours.ThursdayInfo;
                    break;
                case DayOfWeek.Friday:
                    workHoursViewModel.NbHours = workHours.FridayHours;
                    workHoursViewModel.Info = workHours.FridayInfo;
                    break;
                case DayOfWeek.Saturday:
                    workHoursViewModel.NbHours = workHours.SaturdayHours;
                    workHoursViewModel.Info = workHours.SaturdayInfo;
                    break;
                case DayOfWeek.Sunday:
                    workHoursViewModel.NbHours = workHours.SundayHours;
                    workHoursViewModel.Info = workHours.SundayInfo;
                    break;
            }
            workHoursViewModel.PossibleHours = new SelectList(Utils.GetPossibleHours(), 0);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _workingHoursRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
