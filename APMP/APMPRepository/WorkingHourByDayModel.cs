using System;

namespace APMPRepository.Models
{
    public class WorkingHourByDayModel
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
        public DateTime Day { get; set; }
        public float NbHours { get; set; }
        public string Info { get; set; }
        public WorkSiteModel WorkSite { get; set; }
    }
}

