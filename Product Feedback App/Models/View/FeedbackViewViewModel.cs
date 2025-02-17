using Product_Feedback_App.Models.Domain;

namespace Product_Feedback_App.Models.View
{
    public class FeedbackViewViewModel
    {
        public Feedback Feedback { get; set; }
        public bool UserHasUpvoted { get; set; }

        // For feedback comment
        public string CommentContent { get; set; }
    }
}
