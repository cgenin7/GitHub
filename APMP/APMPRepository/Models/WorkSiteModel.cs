using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace APMPRepository.Models
{
    public class WorkSiteModel
    {
        [Key]
        public int WorkSiteId { get; set; }
        [Required(ErrorMessage = "Le nom du chantier doit être rempli") ]
        [DisplayName("Nom du chantier")]
        public string Location { get; set; }
        [DisplayName("Personne contact")]
        public string ContactInfo { get; set; }
        [DisplayName("Téléphone")]
        public string PhoneNb { get; set; }
        [DisplayName("Status")]
        public bool IsActive { get; set; }
        [DisplayName("Information additionnelle")]
        public string AdditionalInfo { get; set; }
        public bool Disabled { get; set; }
    }
}
