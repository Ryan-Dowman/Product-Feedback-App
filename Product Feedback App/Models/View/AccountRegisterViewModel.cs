using System.ComponentModel.DataAnnotations;

namespace Product_Feedback_App.Models.View
{
    public class AccountRegisterViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; }

        [Required]
        public string ProfilePictureUrl { get; set; }
    }
}
