using Product_Feedback_App.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Product_Feedback_App.Models.View
{
    public class FeedbackEditViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        public Category Category { get; set; }

        [Required]
        public string Details { get; set; }
    }
}
