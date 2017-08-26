using System.ComponentModel.DataAnnotations;

namespace CodingCraftEX06HangFire.ViewModels.Account
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
