using APMPBilling.ViewModels;
using APMPRepository.Repositories;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using APMPRepository.Models;

namespace APMPBilling.Controllers
{
    public class SettingsController : Controller
    {
        private SettingsRepository settingsDb = new SettingsRepository();

        // GET: Settings
        public async Task<ActionResult> Index()
        {
            var model = new NotificationViewModel();

            model.Notification = await settingsDb.GetNotificationAsync(User.Identity.GetUserId());
            if (model.Notification == null)
                model.Notification = new NotificationModel(User.Identity.GetUserId());
            else
                model.BeNotified = true;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Save(NotificationViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Notification.UserId == null)
                    model.Notification.UserId = User.Identity.GetUserId();

                if (model.BeNotified)
                    await settingsDb.SaveSettingsAsync(model.Notification);
                else
                    await settingsDb.RemoveSettingsAsync(model.Notification);
                return View("Index", model);
            }
            throw new System.Exception("Le modèle n'est pas valide.");
        }

        internal static async Task NotifyHoursChanged(string userId, WorkHoursViewModel workHoursView)
        {
            using (var repo = new SettingsRepository())
            {
                var notifications = await repo.GetNotificationsAsync();
                foreach (var notification in notifications)
                {
                    if (notification.UserId != userId)
                        await MyEmailService.SendNotificationEmail(notification.NotificationEmail, workHoursView);
                }
            }
        }
    }
}