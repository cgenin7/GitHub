using APMPRepository.Models;

namespace APMPBilling.ViewModels
{
    public class WorkSiteViewModel
    {
        public WorkSiteViewModel()
        {
            ActiveState = true;
        }

        public WorkSiteModel WorkSite { get; set; }
        public bool ActiveState { get; set; }
    }
}