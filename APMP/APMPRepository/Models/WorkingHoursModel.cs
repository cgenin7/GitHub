using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace APMPRepository.Models
{
    public class WorkingHoursModel
    {
        [Key]
        public int WorkingHourId { get; set; }
        
        public DateTime StartOfWeek { get; set; }

        [DisplayName("Lundi")]
        public float MondayHours { get; set; }
        [DisplayName("Info")]
        public string MondayInfo { get; set; }
        [DisplayName("Mardi")]
        public float TuesdayHours { get; set; }
        [DisplayName("Info")]
        public string TuesdayInfo { get; set; }
        [DisplayName("Mercredi")]
        public float WednesdayHours { get; set; }
        [DisplayName("Info")]
        public string WednesdayInfo { get; set; }
        [DisplayName("Jeudi")]
        public float ThursdayHours { get; set; }
        [DisplayName("Info")]
        public string ThursdayInfo { get; set; }
        [DisplayName("Vendredi")]
        public float FridayHours { get; set; }
        [DisplayName("Info")]
        public string FridayInfo { get; set; }
        [DisplayName("Samedi")]
        public float SaturdayHours { get; set; }
        [DisplayName("Info")]
        public string SaturdayInfo { get; set; }
        [DisplayName("Dimanche")]
        public float SundayHours { get; set; }
        [DisplayName("Info")]
        public string SundayInfo { get; set; }

        // Navigation properties
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int WorkSiteId { get; set; }
        public WorkSiteModel WorkSite { get; set; }

    }
}
