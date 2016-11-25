using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace APMPRepository.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [DataType(DataType.EmailAddress, ErrorMessage = "Courriel non valide.")]
        [DisplayName("Courriel")]
        public override string Email
        {
            get
            {
                return base.Email;
            }

            set
            {
                base.Email = value;
            }
        }

        [DisplayName("Status")]
        public override bool EmailConfirmed
        {
            get
            {
                return base.EmailConfirmed;
            }

            set
            {
                base.EmailConfirmed = value;
            }
        }

        [DisplayName("Prénom")]
        [Required(ErrorMessage = "Le prénom doit être remplis.")]
        [MaxLength(255)]
        public string FirstName { get; set; }

        [DisplayName("Nom de famille")]
        [Required(ErrorMessage = "Le nom de famille doit être remplis.")]
        [MaxLength(255)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Le no de téléphone doit être remplis.")]
        [DisplayName("Téléphone")]
        [MaxLength(50)]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Le format est invalide.")]
        public string PhoneNb { get; set; }

        [Required(ErrorMessage = "L'adresse doit être remplie.")]
        [MaxLength(512)]
        [DisplayName("Adresse")]
        public string Address { get; set; }

        [Required(ErrorMessage = "La ville doit être remplie.")]
        [MaxLength(512)]
        [DisplayName("Ville")]
        public string City { get; set; }


        [Required(ErrorMessage = "La province doit être remplie.")]
        [MaxLength(50)]
        [DisplayName("Province")]
        public string State { get; set; }

        [Required(ErrorMessage = "Le code postal doit être rempli.")]
        [MaxLength(50)]
        [DisplayName("Code postal")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Veuillez choisir un niveau de compétence.")]
        [DisplayName("Niveau de compétence")]
        public SkillLevel SkillLevel { get; set; }

        public bool Disabled { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

    }

    public enum SkillLevel
    {
        [Display(Name = "...")]
        NON_DEFINI = 0,
        [Display(Name = "1ère année")]
        UN_AN = 1,
        [Display(Name = "2ème année")]
        DEUX_ANS = 2,
        [Display(Name = "3ème année")]
        TROIS_ANS = 3,
        [Display(Name = "4ème année")]
        QUATRE_ANS = 4,
        [Display(Name = "Compagnon")]
        COMPAGNON = 5
    }


}