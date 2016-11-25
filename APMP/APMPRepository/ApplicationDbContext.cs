using APMPRepository.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace APMPRepository
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<WorkingHoursModel> WorkingHours { get; set; }
        public DbSet<WorkSiteModel> WorkSites { get; set; }
        public DbSet<EquipmentModel> Equipments { get; set; }
        public DbSet<WorkSiteEquipmentModel> WorkSiteEquipments { get; set; }
        public DbSet<PersonResponsibleModel> PersonsResponsible { get; set; }
        public DbSet<NotificationModel> Notifications { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
