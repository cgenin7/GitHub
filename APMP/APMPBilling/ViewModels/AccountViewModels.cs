using APMPRepository.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace APMPBilling.ViewModels
{
    public class ForgotViewModel
    {
        [Required(ErrorMessage = "Le courriel doit être rempli.")]
        [Display(Name = "Courriel")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Courriel non valide.")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Le courriel doit être rempli.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Courriel non valide.")]
        [Display(Name = "Courriel")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Le mot de passe doit être rempli.")]
        [DataType(DataType.Password, ErrorMessage = "Mot de passe non valide.")]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [Display(Name = "Se souvenir de moi")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Le courriel doit être rempli.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Courriel non valide.")]
        [Display(Name = "Courriel")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Le mot de passe doit être rempli.")]
        [StringLength(100, ErrorMessage = "Le {0} doit avoir au moins {2} lettres.", MinimumLength = 6)]
        [DataType(DataType.Password, ErrorMessage = "Mot de passe non valide.")]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le mot de passe")]
        [Compare("Password", ErrorMessage = "Le mot de passe et sa confirmation ne correspondent pas.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Le prénom doit être rempli.")]
        [DisplayName("Prénom")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Le nom de famille doit être rempli.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Le no de téléphone doit être rempli.")]
        [DisplayName("Téléphone")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Le no de téléphone n'est pas valide.")]
        public string PhoneNb { get; set; }

        [Required(ErrorMessage = "L'adresse doit être remplie.")]
        [DisplayName("Adresse")]
        public string Address { get; set; }

        [Required(ErrorMessage = "La ville doit être remplie.")]
        [DisplayName("Ville")]
        public string City { get; set; }

        [Required(ErrorMessage = "La province doit être remplie.")]
        [DisplayName("Province")]
        public string State { get; set; }

        [Required(ErrorMessage = "Le code postal doit être rempli.")]
        [DataType(DataType.PostalCode, ErrorMessage = "Le code postal n'est pas valide.")]
        [DisplayName("Code postal")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Veuillez choisir un niveau de compétence.")]
        [DisplayName("Niveau de compétence")]
        [Range(1, int.MaxValue, ErrorMessage = "Selectionner un niveau de compétences")]
        public SkillLevel SkillLevel { get; set; }
        
    }

    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Le courriel doit être rempli.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Courriel non valide.")]
        [Display(Name = "Courriel")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Le mot de passe doit être rempli.")]
        [StringLength(100, ErrorMessage = "Le {0} doit avoir au moins {2} lettres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le mot de passe")]
        [Compare("Password", ErrorMessage = "Le mot de passe et sa confirmation ne correspondent pas.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Le courriel doit être rempli.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Courriel non valide.")]
        [Display(Name = "Courriel")]
        public string Email { get; set; }
    }
}
