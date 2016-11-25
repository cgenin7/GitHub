using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace APMPRepository.Models
{
    public class EquipmentModel
    {
        [Key]
        public int EquipmentId { get; set; }
        [DisplayName("Type d'équipement")]
        public string EquipmentType { get; set; }
        [DisplayName("Information additionnelle")]
        public string AdditionalInfo { get; set; }
    }
}
