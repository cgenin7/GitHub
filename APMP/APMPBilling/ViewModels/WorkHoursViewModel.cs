using APMPRepository.Models;
using System;
using System.ComponentModel;
using System.Web.Mvc;

namespace APMPBilling.ViewModels
{
    public class WorkHoursViewModel
    {
        public WorkHoursViewModel()
        { }

        public WorkHoursViewModel(string userName, string userId, DateTime displayDay)
        {
            UserName = userName;
            DisplayDay = displayDay;
            UserId = userId;
        }

        public SelectList Sites { get; set; }
        public SelectList PossibleHours { get; set; }
        public int SelectedSiteId { get; set; }
        public string SiteName { get; set; }

        public string UserName { get; set; }
        public string UserId { get; set; }

        public DateTime DisplayDay { get; set; }
        public string Month { get; set; }
       
        [DisplayName("heures")]
        public float NbHours { get; set; }

        [DisplayName("Commentaire")]
        public string Info {get; set; }
    }
}