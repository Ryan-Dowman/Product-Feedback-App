using Product_Feedback_App.Models.Domain;
using Product_Feedback_App.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Product_Feedback_App.Models.View
{
    public class FeedbackCreateViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public string Details { get; set; }
    }
}
