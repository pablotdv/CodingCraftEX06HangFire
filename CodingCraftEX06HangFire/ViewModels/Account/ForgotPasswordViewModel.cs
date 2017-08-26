using System.ComponentModel.DataAnnotations;

namespace CodingCraftEX06HangFire.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
