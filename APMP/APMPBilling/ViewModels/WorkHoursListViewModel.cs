using APMPRepository.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace APMPBilling.ViewModels
{
    public class WorkHoursListViewModel
    {
        public WorkHoursListViewModel()
        { }

        public WorkHoursListViewModel(string userId, string userName, DateTime startOfWeek)
        {
            UserId = userId;
            UserName = userName;
            StartOfWeek = startOfWeek;
            WorksiteHours = new List<WorkingHoursModel>();
        }

        public SelectList Users { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }

        public DateTime StartOfWeek { get; set; }
        public IList<WorkingHoursModel> WorksiteHours { get; set; }
        public string Period { get; set; }
        public bool AllEmployees { get; set; }
        public int NbWeeks { get; set; }
        
    }
}