using Product_Feedback_App.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Product_Feedback_App.Models.View
{
    public class FeedbackEditViewModel
    {
        public string Title { get; set; }

        public string Category { get; set; }

        public string StatusName { get; set; }

        public string Details { get; set; }
    }
}
