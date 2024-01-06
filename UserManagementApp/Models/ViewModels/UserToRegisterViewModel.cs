using System.ComponentModel.DataAnnotations;

namespace UserManagementApp.Models.ViewModels
{
    public class UserToRegisterViewModel
    {
        [Required]
        [StringLength(15, MinimumLength = 2, ErrorMessage ="2 - 15 character allowed")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "2 - 15 character allowed")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;

    }
}
