using System.ComponentModel.DataAnnotations;

namespace Product_Feedback_App.Models.View
{
    public class AccountLoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
