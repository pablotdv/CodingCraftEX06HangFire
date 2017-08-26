using System.ComponentModel.DataAnnotations;

namespace CodingCraftEX06HangFire.ViewModels.Manage
{
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }
}