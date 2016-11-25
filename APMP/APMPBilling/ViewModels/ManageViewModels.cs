using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace APMPBilling.ViewModels
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
   
        public bool BrowserRemembered { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required(ErrorMessage = "Le mot de passe doit être rempli")]
        [StringLength(100, ErrorMessage = "Le {0} doit avoir au moins {2} lettres.", MinimumLength = 6)]
        [DataType(DataType.Password, ErrorMessage = "Le mot de passe n'est pas valide.")]
        [Display(Name = "Nouveau mot de passe")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le nouveau mot de passe")]
        [Compare("NewPassword", ErrorMessage = "Le mot de passe et sa confirmation ne correspondent pas.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Le mot de passe doit être rempli")]
        [DataType(DataType.Password, ErrorMessage = "Le mot de passe n'est pas valide.")]
        [Display(Name = "Mot de passe actuel")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Le nouveau mot de passe doit être rempli")]
        [StringLength(100, ErrorMessage = "Le {0} doit avoir au moins {2} lettres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nouveau mot de passe")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "La confirmation doit être remplie")]
        [Display(Name = "Confirmer le nouveau mot de passe")]
        [Compare("NewPassword", ErrorMessage = "Le mot de passe et sa confirmation ne correspondent pas.")]
        public string ConfirmPassword { get; set; }
    }
}