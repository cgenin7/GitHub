using APMPRepository.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Linq;

namespace APMPRepository
{
    public class WorkingHoursRepository : BaseRepository
    {
        public async Task<List<WorkingHoursModel>> GetWorkingHoursWithWorkSiteAsync(string userId, DateTime startOfWeekDate)
        {
            return await _db.WorkingHours.Include(w => w.WorkSite).AsNoTracking().OrderBy(w => w.StartOfWeek).
                Where(w => w.UserId == userId && w.StartOfWeek == startOfWeekDate && w.WorkSite.Disabled == false).
                ToListAsync();
        }

        public async Task<List<WorkingHourByDayModel>> GetWorkingHoursWithWorkSiteAsync(string userId, DateTime startDate, DateTime endDate)
        {
            var workingHours = await _db.WorkingHours.Include(w => w.WorkSite).AsNoTracking().
                Where(w => w.UserId == userId && w.StartOfWeek >= startDate && w.StartOfWeek <= endDate && w.WorkSite.Disabled == false).
                OrderBy(w => w.StartOfWeek).ToListAsync();

            return GetByDay(workingHours, startDate, endDate);
        }

        public async Task<WorkingHoursModel> GetWorkingHoursWithWorkSiteAsync(int workSiteId, string userId, DateTime startOfWeekDate)
        {
            return await _db.WorkingHours.Include(w => w.WorkSite).AsNoTracking()
                .Where(w => w.WorkSiteId == workSiteId && w.UserId == userId && w.StartOfWeek == startOfWeekDate && w.WorkSite.Disabled == false).
                FirstOrDefaultAsync();
        }

        public async Task<List<WorkingHourByDayModel>> GetWorkingHoursWithWorkSiteAsync(int workSiteId, string userId, DateTime startDate, DateTime endDate)
        {
            var startOfWeek = startDate.StartOfWeek(DayOfWeek.Monday).Date;
            var workingHours = await _db.WorkingHours.Include(w => w.WorkSite).AsNoTracking()
                .Where(w => w.WorkSiteId == workSiteId && w.UserId == userId &&
                        w.StartOfWeek >= startOfWeek && w.StartOfWeek <= endDate &&
                        w.WorkSite.Disabled == false).
                OrderBy(w => w.StartOfWeek).ToListAsync();
            return GetByDay(workingHours, startDate, endDate);
        }

        public async Task<WorkingHoursModel> GetWorkingHoursAsync(string userId, int workSiteId, DateTime startOfWeek)
        {
            return await _db.WorkingHours.Include(w => w.WorkSite).SingleOrDefaultAsync(w => w.UserId == userId
                    && w.WorkSiteId == workSiteId && w.StartOfWeek == startOfWeek && w.WorkSite.Disabled == false);
        }

        public async Task AddWorkingHoursAsync(WorkingHoursModel WorkingHours)
        {
            _db.WorkingHours.Add(WorkingHours);
            await SaveChangesAsync();
        }

        public async Task EditWorkingHoursAsync(WorkingHoursModel WorkingHours)
        {
            _db.Entry(WorkingHours).State = EntityState.Modified;
            await SaveChangesAsync();
        }

        public async Task RemoveWorkingHoursAsync(string userId, int workSiteId, DateTime startOfWeek)
        {
            WorkingHoursModel WorkingHoursModel = await GetWorkingHoursAsync(userId, workSiteId, startOfWeek);
            _db.WorkingHours.Remove(WorkingHoursModel);
            await SaveChangesAsync();
        }

        private List<WorkingHourByDayModel> GetByDay(List<WorkingHoursModel> workingHours, DateTime startDate, DateTime endDate)
        {
            List<WorkingHourByDayModel> result = new List<WorkingHourByDayModel>();
            foreach (var workingHour in workingHours)
            {
                AddDay(result, workingHour.StartOfWeek, workingHour.MondayHours, workingHour.MondayInfo, startDate, endDate, workingHour);
                AddDay(result, workingHour.StartOfWeek.AddDays(1), workingHour.TuesdayHours, workingHour.TuesdayInfo, startDate, endDate, workingHour);
                AddDay(result, workingHour.StartOfWeek.AddDays(2), workingHour.WednesdayHours, workingHour.WednesdayInfo, startDate, endDate, workingHour);
                AddDay(result, workingHour.StartOfWeek.AddDays(3), workingHour.ThursdayHours, workingHour.ThursdayInfo, startDate, endDate, workingHour);
                AddDay(result, workingHour.StartOfWeek.AddDays(4), workingHour.FridayHours, workingHour.FridayInfo, startDate, endDate, workingHour);
                AddDay(result, workingHour.StartOfWeek.AddDays(5), workingHour.SaturdayHours, workingHour.SaturdayInfo, startDate, endDate, workingHour);
                AddDay(result, workingHour.StartOfWeek.AddDays(6), workingHour.SundayHours, workingHour.SundayInfo, startDate, endDate, workingHour);
            }

            return result;
        }

        private void AddDay(List<WorkingHourByDayModel> result, DateTime day, float nbHours, string info, DateTime startDate,
            DateTime endDate, WorkingHoursModel workingHour)
        {
            if (day >= startDate && day <= endDate && (nbHours > 0 || !string.IsNullOrEmpty(info)))
                result.Add(new WorkingHourByDayModel()
                {
                    Day = day,
                    NbHours = nbHours,
                    Info = info,
                    UserId = workingHour.UserId,
                    WorkSite = workingHour.WorkSite
                });
        }
    }
}
