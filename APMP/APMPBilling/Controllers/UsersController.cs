using System.Net;
using System.Web.Mvc;
using APMPRepository;
using APMPRepository.Models;
using APMPBilling.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APMPBilling.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private UserRepository _userRepo = new UserRepository();

        // GET: Users
        public async Task<ActionResult> Index()
        {
            var users = await _userRepo.GetUsersAsync();

            var usersViewModel = new List<ApplicationUserViewModel>();

            foreach (var user in users)
            {
                usersViewModel.Add(await GetApplicationUserView(user));
            }

            return View(usersViewModel);
        }

        // GET: Users/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = await _userRepo.GetUserAsync(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(await GetApplicationUserView(applicationUser));
        }

        // GET: Users/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = await _userRepo.GetUserAsync(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }

            return View(await GetApplicationUserView(applicationUser));
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // [Bind(Include = "User.Id,User.FirstName,User.LastName,User.PhoneNb,User.Address,User.City,User.State,User.PostalCode,User.SkillLevel,User.EmailConfirmed,IsAdmin")] 
        public async Task<ActionResult> Edit(ApplicationUserViewModel userView)
        {
            if (ModelState.IsValid)
            {
                ModelState.Clear();
                var user = await _userRepo.GetUserAsync(userView.User.Id);

                user.FirstName = userView.User.FirstName;
                user.LastName = userView.User.LastName;
                user.PhoneNb = userView.User.PhoneNb;
                user.Address = userView.User.Address;
                user.City = userView.User.City;
                user.State = userView.User.State;
                user.PostalCode = userView.User.PostalCode;
                user.SkillLevel = userView.User.SkillLevel;
                user.EmailConfirmed = userView.User.EmailConfirmed;

                if (userView.IsAdmin)
                {
                    await _userRepo.SetAdmin(user.Id, true);
                }
                else
                {
                    await _userRepo.SetAdmin(user.Id, false);
                }
                await _userRepo.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(userView);
        }

        // GET: Users/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = await _userRepo.GetUserAsync(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            ModelState.Clear();
            await _userRepo.DisableUserAsync(id);
            return RedirectToAction("Index");
        }

        private async Task<ApplicationUserViewModel> GetApplicationUserView(ApplicationUser user)
        {
            var isAdmin = await _userRepo.IsAdmin(user.Id);
            return new ApplicationUserViewModel { User = user, IsAdmin = isAdmin };
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _userRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
