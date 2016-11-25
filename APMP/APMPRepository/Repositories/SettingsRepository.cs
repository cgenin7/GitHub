using APMPRepository.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq;

namespace APMPRepository.Repositories
{
    public class SettingsRepository : BaseRepository
    {
        public async Task<List<NotificationModel>> GetNotificationsAsync()
        {
            return await _db.Notifications.AsNoTracking().Include(n => n.User).ToListAsync();
        }

        public async Task<NotificationModel> GetNotificationAsync(string userId)
        {
            return await _db.Notifications.AsNoTracking().Include(n => n.User).Where(n => n.UserId == userId).SingleOrDefaultAsync();
        }

        public async Task SaveSettingsAsync(NotificationModel model)
        {
            var notification = await GetNotificationAsync(model.UserId);
            if (notification == null)
                _db.Notifications.Add(model);
            else
                notification.NotificationEmail = model.NotificationEmail;
            await SaveChangesAsync();
        }

        public async Task RemoveSettingsAsync(NotificationModel model)
        {
            var notification = await _db.Notifications.Where(n => n.UserId == model.UserId).SingleOrDefaultAsync();
            if (notification != null)
            {
                _db.Notifications.Remove(notification);
                await SaveChangesAsync();
            }
        }
    } 
}
