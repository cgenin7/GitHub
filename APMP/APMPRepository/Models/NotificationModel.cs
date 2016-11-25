using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace APMPRepository.Models
{
    public class NotificationModel
    {
        public NotificationModel()
        { }

        public NotificationModel(string userId)
        {
            UserId = userId;
        }

        [Key]
        public int Id { get; set; }
        
        [DataType(DataType.EmailAddress, ErrorMessage = "Courriel non valide.")]
        [DisplayName("Courriel")]
        public string NotificationEmail { get; set; }

        // Navigation property
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
