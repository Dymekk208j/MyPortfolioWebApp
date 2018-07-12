using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyPortfolioWebApp.Models.ViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Zapamiętaj?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Login")]
        [Index(IsUnique = true)]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Hasło musi składać się z minimum 6 znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Hasła nie pasują do siebie.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [Display(Name = "E-mail")]
        [EmailAddress]
        [Index(IsUnique = true)]
        public string Email { get; set; }

    }

    public class UpdateViewModel
    {
        [Required]
        public string Id { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [Display(Name = "Login")]
        [Index(IsUnique = true)]
        public string UserName { get; set; }

        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [Display(Name = "E-mail")]
        [EmailAddress]
        [Index(IsUnique = true)]
        public string Email { get; set; }

        [Display(Name = "Zablokowany")]
        public bool Blocked { get; set; }
    }
}
