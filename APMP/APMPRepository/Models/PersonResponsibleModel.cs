using System.ComponentModel.DataAnnotations;

namespace APMPRepository.Models
{
    public class PersonResponsibleModel
    {
        [Key]
        public int PersonResponsibleId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string PhoneNb { get; set; }
    }
}
