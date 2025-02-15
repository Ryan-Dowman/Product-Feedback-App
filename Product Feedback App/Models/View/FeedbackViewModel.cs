using Product_Feedback_App.Models.Domain;

namespace Product_Feedback_App.Models.View
{
    public class FeedbackViewModel
    {
        public Feedback Feedback { get; set; }
        public bool UserHasUpvoted { get; set; }
    }
}
