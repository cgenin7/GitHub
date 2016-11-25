using APMPRepository.Models;
using System.ComponentModel;

namespace APMPBilling.ViewModels
{
    public class ApplicationUserViewModel
    {
        public ApplicationUser User { get; set; }

        [DisplayName("Rôle")]
        public bool IsAdmin { get; set; }
    }
}