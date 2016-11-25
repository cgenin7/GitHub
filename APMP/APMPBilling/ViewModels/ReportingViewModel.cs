using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace APMPBilling.ViewModels
{
    public class ReportingViewModel
    {
        public SelectList WorkSites { get; set; }
        [DisplayName("Chantiers")]
        public int WorkSiteId { get; set; }

        public SelectList Employees { get; set; }
        [DisplayName("Employés")]
        public string EmployeeId { get; set; }

        [DisplayName("Date de début")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DisplayName("Date de fin")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        public SelectList SortBy { get; set; }
        [DisplayName("Trier par")]
        public ESortOrder SortOrder { get; set; }
    }

    public enum ESortOrder
    {
        WORKSITE,
        EMPLOYEE
    }
}