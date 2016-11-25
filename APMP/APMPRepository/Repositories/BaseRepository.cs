using log4net;
using System;
using System.Data.Entity.Validation;
using System.Threading.Tasks;

namespace APMPRepository
{
    public class BaseRepository : IDisposable
    {
        protected ApplicationDbContext _db = new ApplicationDbContext();
        protected readonly ILog _logger = LogManager.GetLogger("APMPLogger");
        
        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in e.EntityValidationErrors)
                    {
                        _logger.Error(String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                ve.Entry.Entity.GetType().Name, ve.Entry.State));
                        foreach (var error in ve.ValidationErrors)
                        {
                            _logger.Error(String.Format("- Property: \"{0}\", Error: \"{1}\"",
                                error.PropertyName, error.ErrorMessage));
                        }
                    }
                }
                _db.Dispose();
                _db = new ApplicationDbContext();
                return false;
            }
            return true;
        }

        public void Dispose()
        {
            _db.Dispose();
        }

    }
}
