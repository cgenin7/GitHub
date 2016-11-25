using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APMPRepository.Models
{
    public class WorkSiteEquipmentModel
    {
        public int Quantity { get; set; }
        public WorksiteEquipmentStatus Status { get; set; }

        // Navigation properties
        [Key, Column(Order = 0)]
        public int WorkSiteId { get; set; }
        [Key, Column(Order = 1)]
        public int EquipmentId { get; set; }
        public int PersonResponsibleId { get; set; }
        public WorkSiteModel WorkSite { get; set; }
        public EquipmentModel Equipment { get; set; }
        public PersonResponsibleModel PersonResponsible { get; set; }
    }

    public enum WorksiteEquipmentStatus
    {
        UNKNOWN,
        NEEDED,
        ORDERED,
        RECEIVED
    }
}
