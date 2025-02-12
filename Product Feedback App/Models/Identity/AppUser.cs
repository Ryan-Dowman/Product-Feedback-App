using Microsoft.AspNetCore.Identity;

namespace Product_Feedback_App.Models.Identity
{
    public class AppUser : IdentityUser
    {
        public string ProfilePictureUrl { get; set; }
    }
}
