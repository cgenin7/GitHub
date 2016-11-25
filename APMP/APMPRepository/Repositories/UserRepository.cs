using APMPRepository.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;

namespace APMPRepository
{
    public class UserRepository : BaseRepository
    {
        public UserStore<ApplicationUser> Store { get; private set; }
        public UserManager<ApplicationUser> Manager { get; private set; }
        
        public UserRepository()
        {
            Store = new UserStore<ApplicationUser>(_db);
            Manager = new UserManager<ApplicationUser>(Store);
        }

        public async Task<List<ApplicationUser>> GetUsersAsync()
        {
            return await _db.Users.Where(u => u.Disabled == false).AsNoTracking().ToListAsync();
        }

        public async Task<List<ApplicationUser>> GetDisabledUsersAsync()
        {
            return await _db.Users.Where(u => u.Disabled == true).AsNoTracking().ToListAsync();
        }

        public async Task<ApplicationUser> GetUserAsync(string userId)
        {
            return await Manager.Users.FirstOrDefaultAsync(u => u.Id == userId && u.Disabled == false);
        }

        public async Task<bool> IsAdmin(string userId)
        {
            var roles = await Manager.GetRolesAsync(userId);
            return roles.Contains(ADMIN);
        }

        public async Task SetAdmin(string userId, bool isAdmin)
        {
            if (isAdmin)
                await Manager.AddToRoleAsync(userId, ADMIN);
            else
                await Manager.RemoveFromRoleAsync(userId, ADMIN);
        }

        public async Task DisableUserAsync(string userId)
        {
            ApplicationUser user = await GetUserAsync(userId);

            user.Disabled = true;

            await EditUserAsync(user);
        }

        public async Task EnableUserAsync(string userId)
        {
            ApplicationUser user = await GetUserAsync(userId);

            user.Disabled = false;

            await EditUserAsync(user);
        }

        public async Task EditUserAsync(ApplicationUser user)
        {
            _db.Entry(user).State = EntityState.Modified;
            await SaveChangesAsync();
        }

        private string ADMIN = "Admin";
    }
}
