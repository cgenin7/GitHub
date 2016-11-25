using APMPRepository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq;

namespace APMPRepository
{
    public class WorkSiteRepository : BaseRepository
    {
        public async Task<List<WorkSiteModel>> GetWorkSitesAsync()
        {
            return await _db.WorkSites.Where(w => w.Disabled == false).AsNoTracking().ToListAsync();
        }

        public async Task<WorkSiteModel> GetWorkSiteAsync(int id)
        {
            var workSite = await _db.WorkSites.FindAsync(id);
            if (workSite.Disabled)
                return null;
            return workSite;
        }

        public async Task<bool> AddWorkSiteAsync(WorkSiteModel workSite)
        {
            _db.WorkSites.Add(workSite);
            return await SaveChangesAsync();
        }

        public async Task<bool> EditWorkSiteAsync(WorkSiteModel workSite)
        {
            _db.Entry(workSite).State = EntityState.Modified;
            return await SaveChangesAsync();
        }

        public async Task<bool> RemoveWorkSiteAsync(int id)
        {
            WorkSiteModel workSiteModel = await GetWorkSiteAsync(id);
            workSiteModel.Disabled = true;

            return await EditWorkSiteAsync(workSiteModel);
        }
    }
}
